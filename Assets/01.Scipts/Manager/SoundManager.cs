using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
public class SoundManager : Singleton<SoundManager>
{
    // 사용할 사운드 종류들
    public enum SoundType
    {
        BGMVolume,
        SFXVolume,
    }
    
    public enum SnepShotType
    {
        Normal,
        Muffled,
    }

    public AudioMixer mixer;
    public GameObject sfxPrefab;
    private AudioSource bgmSource;
    private BGMSound bgmSound;
    private AudioMixerSnapshot muffledSnapshot;
    private AudioMixerSnapshot normalSnapshot;

    protected override async void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
        await Init();
        
        PlayBGM(StringNameSpace.SoundAddress.TutorialBGM);
    }
    
    //BGM 실행 메서드
    public async void PlayBGM(string key)
    {
        var clip = await ResourceLoader.LoadAssetAddress<AudioClip>(key);
        if (clip == null)
        {
            Debug.LogError($"BGM 로딩 실패: {key}");
            return;
        }

        bgmSound.Play(clip);
    }

    //효과음 실행 메서드
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        GameObject sfxObj = Instantiate(sfxPrefab);
        if(sfxObj.TryGetComponent(out SFXSound sfx))
            sfx.Play(clip, volume);
    }

    //사운드 조절 메서드
    private float VolumeToDecibel(float volume) // 소리 100단위를 데시벨로 변환
    {
        return Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
    }

    /// <summary>
    /// 믹서의 소리를 조절하는 함수
    /// </summary>
    /// <param name="type">BGM인지 SFX인지 유무</param>
    /// <param name="volume">얼마큼 조절할지 유무</param>
    public void SetVolume(SoundType type, float volume)
    {
        mixer.SetFloat(SoundType.BGMVolume.ToString(), VolumeToDecibel(volume));
        mixer.SetFloat(SoundType.SFXVolume.ToString(), VolumeToDecibel(volume));
    }

    //생성
    public async Task Init()
    {
        GameObject obj = await ResourceLoader.LoadAssetAddress<GameObject>(StringNameSpace.SoundAddress.SFXPrefab);
        sfxPrefab = obj;
        mixer = await ResourceLoader.LoadAssetAddress<AudioMixer>(StringNameSpace.SoundAddress.MainMixer);
        
        //머플 사운드 스냅샷 저장
        normalSnapshot = mixer.FindSnapshot(SnepShotType.Normal.ToString());
        muffledSnapshot = mixer.FindSnapshot(SnepShotType.Muffled.ToString());

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.outputAudioMixerGroup = await ResourceLoader.LoadAssetAddress<AudioMixerGroup>(StringNameSpace.SoundAddress.BGMMixer);
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
        bgmSource.volume = 0.5f;

        bgmSound = gameObject.AddComponent<BGMSound>();
        bgmSound.Init(bgmSource); // AudioSource 주입
    }

    //BGM 정지
    public void StopBGM()
    {
        bgmSound.Stop();
    }

    public void MuffleSound(bool isMuffled, float time = 0)
    {
        if (isMuffled)
            muffledSnapshot.TransitionTo(time);
        else
            normalSnapshot.TransitionTo(time);
    }
}