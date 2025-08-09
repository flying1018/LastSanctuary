using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

public enum BGM
{
    Tutorials_Sound,
    Boss01_PhaseShift,
    Boss01_Phase1,
    Boss01_Phase2
}

public class SoundManager : Singleton<SoundManager>
{
    // 사용할 사운드 종류들
    public enum SoundType
    {
        BGMMixer,
        SFXMixer,
    }
    
    public enum SnepShotType
    {
        Normal,
        Muffled,
    }

    public AudioMixer mixer;
    public GameObject sfxPrefab;
    private AudioSource _bgmSource;
    private BGMSound _bgmSound;
    private AudioMixerSnapshot _muffledSnapshot;
    private AudioMixerSnapshot _normalSnapshot;
    private Dictionary<BGM, AudioClip> _BGMs = new Dictionary<BGM, AudioClip>();

    protected override async void Awake()
    {
        base.Awake();
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else if (Instance == this)
        { 
            DontDestroyOnLoad(gameObject);
        }

        await Init();
    }
    
    //BGM 실행 메서드
    public void PlayBGM(BGM bgm)
    {
        if (_BGMs.TryGetValue(bgm, out AudioClip clip))
        {
            _bgmSound.Play(clip);
        }
        else
        {
            DebugHelper.Log("찾을 수 없음.");
        }
    }

    public async void PlayBGM(string key)
    {
        var clip = await ResourceLoader.LoadAssetAddress<AudioClip>(key);
        if (clip == null)
        {
            Debug.LogError($"BGM 로딩 실패: {key}");
            return;
        }
        
        _bgmSound.Play(clip);
    }

    //효과음 실행 메서드
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        GameObject sfxObj = ObjectPoolManager.Get(sfxPrefab, (int)PoolingIndex.SFX);
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
        mixer.SetFloat(type.ToString(), VolumeToDecibel(volume));
    }

    //생성
    public async Task Init()
    {
        GameObject obj = await ResourceLoader.LoadAssetAddress<GameObject>(StringNameSpace.SoundAddress.SFXPrefab);
        sfxPrefab = obj;
        mixer = await ResourceLoader.LoadAssetAddress<AudioMixer>(StringNameSpace.SoundAddress.MainMixer);
        
        //머플 사운드 스냅샷 저장
        _normalSnapshot = mixer.FindSnapshot(SnepShotType.Normal.ToString());
        _muffledSnapshot = mixer.FindSnapshot(SnepShotType.Muffled.ToString());

        _bgmSource = gameObject.AddComponent<AudioSource>();
        _bgmSource.outputAudioMixerGroup = await ResourceLoader.LoadAssetAddress<AudioMixerGroup>(StringNameSpace.SoundAddress.BGMMixer);
        _bgmSource.loop = true;
        _bgmSource.playOnAwake = false;
        _bgmSource.volume = 0.5f;

        _bgmSound = gameObject.AddComponent<BGMSound>();
        _bgmSound.Init(_bgmSource); // AudioSource 주입
        
        GetMapBGM();
    }

    //BGM 정지
    public void StopBGM()
    {
        _bgmSound.Stop();
    }

    public void MuffleSound(bool isMuffled, float speed = 0)
    {
        if (isMuffled)
            _muffledSnapshot.TransitionTo(speed);
        else
            _normalSnapshot.TransitionTo(speed);
    }
    
    public async void GetMapBGM()
    {
        List<AudioClip> audioClips = await ResourceLoader.LoadAssetsLabel<AudioClip>(StringNameSpace.Labels.BGM);
        foreach (AudioClip audioClip in audioClips)
        {
            foreach (BGM bgm in Enum.GetValues(typeof(BGM)))
            {
                if (audioClip.name == bgm.ToString())
                {
                    _BGMs.Add(bgm, audioClip);
                }
            }
        }
        
        PlayBGM(BGM.Tutorials_Sound);
    }
}