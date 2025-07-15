using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
public class SoundManager : Singleton<SoundManager>
{
    // 사용할 사운드 종류들
    public enum SoundType
    {
        BGM,
        Effect,
    }

    public AudioMixer mixer;
    public GameObject sfxPrefab;
    private AudioSource bgmSource;
    private BGMSound bgmSound;
    
    private const string sfxVolumeName = "SFXVolume";
    private const string bgmVolumeName = "BGMVolume";

    protected override async void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
        await Init();
        PlayBGM(StringNameSpace.SoundAddress.TutorialBGM);
    }
    
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

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        GameObject sfxObj = Instantiate(sfxPrefab);
        SFXSound sfx = sfxObj.GetComponent<SFXSound>();

        sfx.Play(clip, volume);
    }

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
        switch (type)
        {
            case SoundType.BGM:
                mixer.SetFloat(bgmVolumeName, VolumeToDecibel(volume));
                break;
            case SoundType.Effect:
                mixer.SetFloat(sfxVolumeName, VolumeToDecibel(volume));
                break;
        }
    }

    public async Task Init()
    {
        GameObject obj = await ResourceLoader.LoadAssetAddress<GameObject>(StringNameSpace.SoundAddress.SFXPrefab);
        sfxPrefab = obj;
        mixer = await ResourceLoader.LoadAssetAddress<AudioMixer>(StringNameSpace.SoundAddress.MainMixer);

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.outputAudioMixerGroup = await ResourceLoader.LoadAssetAddress<AudioMixerGroup>(StringNameSpace.SoundAddress.BGMMixer);
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
        bgmSource.volume = 0.5f;

        bgmSound = gameObject.AddComponent<BGMSound>();
        bgmSound.Init(bgmSource); // AudioSource 주입
    }

    public void StopBGM()
    {
        bgmSound.Stop();
    }
}