using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUI : UnifiedUI
{
    [Header("SettingUI")] [SerializeField] private TextMeshProUGUI resolutionText;
    [SerializeField] private TextMeshProUGUI fullscreenText;
    [SerializeField] private Slider bgmVolume;
    [SerializeField] private Slider sfxVolume;

    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button fullscreenButtonA;
    [SerializeField] private Button fullscreenButtonB;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button titleButton;
    [SerializeField] private Button initButton;
    [SerializeField] private Button revertButton;

    //설정 값
    private Resolution[] _resolutions;
    private int _curResolutionIndex;

    private bool _isFullScreen;

    //초기 설정
    private int _defaultResolutionIndex;
    private bool _defaultFullScreen;
    private float _defaultBgmVolume;
    private float _defaultSfxVolume;

    public SettingUI(UIStateMachine uiStateMachine) : base(uiStateMachine)
    {
    }

    public override void Init(UIStateMachine uiStateMachine)
    {
        base.Init(uiStateMachine);

        leftButton.onClick.AddListener(OnClickLeft);
        rightButton.onClick.AddListener(OnClickRight);
        fullscreenButtonA.onClick.AddListener(OnClickScreen);
        fullscreenButtonB.onClick.AddListener(OnClickScreen);
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChange);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChange);
        titleButton.onClick.AddListener(ReturnToTitle);
        initButton.onClick.AddListener(InitSettings);
        revertButton.onClick.AddListener(RevertSettings);
    }

    public void TitleInit()
    {
        leftButton.onClick.AddListener(OnClickLeft);
        rightButton.onClick.AddListener(OnClickRight);
        fullscreenButtonA.onClick.AddListener(OnClickScreen);
        fullscreenButtonB.onClick.AddListener(OnClickScreen);
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChange);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChange);
        initButton.onClick.AddListener(InitSettings);
        revertButton.onClick.AddListener(RevertSettings);
    }

    public override void Enter()
    {
        base.Enter();
        if (_uiManager != null)
        {
            mouseLeft.SetActive(false);
            mouseRight.SetActive(false);
        }
        
        gameObject.SetActive(true);

        SetupSettings();
        LoadSliderSet();
    }

    public override void Exit()
    {
        base.Exit();
        gameObject.SetActive(false);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyDown(KeyCode.E))
        {
            //성물 UI로 이동
            _uiStateMachine.ChangeState(_uiStateMachine.RelicUI);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //스킬 UI로 이동
            _uiStateMachine.ChangeState(_uiStateMachine.SkillUI);
        }
    }

    //초기 설정
    private void SetupSettings()
    {
        _isFullScreen = Screen.fullScreen;
        InitResolution();
        InitSettings();
    }

    //타이틀로 이동
    private void ReturnToTitle()
    {
        SceneManager.LoadScene(StringNameSpace.Scenes.TitleScene);
    }

    //초기값 설정, 옵션 저장
    private void InitSettings()
    {
        _defaultResolutionIndex = _curResolutionIndex;
        _defaultFullScreen = _isFullScreen;
        LoadSliderSet();
        _defaultBgmVolume = bgmVolume.value;
        _defaultSfxVolume = sfxVolume.value;
        Resolution res = _resolutions[_curResolutionIndex];
        Screen.SetResolution(res.width, res.height, _defaultFullScreen);
        ApplySettingTexts();
    }

    //설정 되돌리기
    private void RevertSettings()
    {
        _curResolutionIndex = _defaultResolutionIndex;
        _isFullScreen = _defaultFullScreen;
        bgmVolume.value = _defaultBgmVolume;
        sfxVolume.value = _defaultSfxVolume;
        SoundManager.Instance.SetVolume(SoundManager.SoundType.BGMMixer, bgmSlider.value);
        SoundManager.Instance.SetVolume(SoundManager.SoundType.SFXMixer, sfxSlider.value);
        LoadSliderSet();
        ApplySettingTexts();
    }

    #region 설정 화면

    //해상도 설정
    private void InitResolution() // 가능한 해상도 불러오기
    {
        var allresolutions = Screen.resolutions;
        var options = new List<Resolution>();
        var resolution = new HashSet<string>();


        for (var i = 0; i < allresolutions.Length; i++)
        {
            string key = $"{allresolutions[i].width} x {allresolutions[i].height}";
            
            if (!resolution.Contains(key)) //중복 제거
            {
                resolution.Add(key);
                options.Add(allresolutions[i]);
            }

            if (allresolutions[i].width == Screen.currentResolution.width &&
                allresolutions[i].height == Screen.currentResolution.height)
            {
                _curResolutionIndex = options.Count - 1;
            }
        }
        _resolutions = options.ToArray();
    }

    public void OnClickLeft()
    {
        SoundManager.Instance.PlaySFX(_data.buttonClickSound);
        _curResolutionIndex--;
        if (_curResolutionIndex < 0)
            _curResolutionIndex = _resolutions.Length - 1;
        ApplySettingTexts();
    }

    public void OnClickRight()
    {
        SoundManager.Instance.PlaySFX(_data.buttonClickSound);
        _curResolutionIndex++;
        if (_curResolutionIndex >= _resolutions.Length)
            _curResolutionIndex = 0;
        ApplySettingTexts();
    }

    //전체화면 설정
    public void OnClickScreen()
    {
        SoundManager.Instance.PlaySFX(_data.buttonClickSound);
        _isFullScreen = !_isFullScreen;
        ApplySettingTexts();
    }

    //설정 화면
    public void ApplySettingTexts()
    {
        Resolution res = _resolutions[_curResolutionIndex];
        resolutionText.text = $"{res.width} x {res.height}";
        fullscreenText.text = _isFullScreen ? "전체화면" : "창모드";
    }

    //배경음 설정
    public void OnBGMVolumeChange(float value)
    {
        SoundManager.Instance.SetVolume(SoundManager.SoundType.BGMMixer, value);
        Debug.Log($"배경음 볼륨: {value:F2}");
    }

    //효과음 설정
    public void OnSFXVolumeChange(float value)
    {
        SoundManager.Instance.SetVolume(SoundManager.SoundType.SFXMixer, value);
        Debug.Log($"효과음 볼륨: {value:F2}");
    }

    //슬라이드 설정
    private void LoadSliderSet()
    {
        float bgmVolumeValue, sfxVolumeValue;

        SoundManager.Instance.mixer.GetFloat(SoundManager.SoundType.BGMMixer.ToString(), out bgmVolumeValue);
        SoundManager.Instance.mixer.GetFloat(SoundManager.SoundType.SFXMixer.ToString(), out sfxVolumeValue);

        bgmVolume.value = DbToLinear(bgmVolumeValue);
        sfxVolume.value = DbToLinear(sfxVolumeValue);
    }

    private float DbToLinear(float db)
    {
        return Mathf.Pow(10f, db / 20f);
    }

    #endregion
}