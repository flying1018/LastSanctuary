using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUI : UnifiedUI
{       
        //설정 디스플레이
        private TextMeshProUGUI  _resolution;
        private TextMeshProUGUI  _fullscreen;
        private Slider _bgmVolume;
        private Slider _sfxVolume;
        //설정 값
        private Resolution[] _resolutions;
        private int _curResolutionIndex = 0;
        private bool _isFullScreen = true;
        //초기 설정
        private int _defaultResolutionIndex;
        private bool _defaultFullscreen;
        private float _defaultBgmVolume;
        private float _defaultSfxVolume;
        
        public SettingUI(UIStateMachine uiStateMachine) : base(uiStateMachine)
        {
          _resolution = _uiManager.ResolutionText;
          _fullscreen = _uiManager.FullscreenText;
          _bgmVolume = _uiManager.BgmVolume;
          _sfxVolume = _uiManager.SfxVolume;
          
          _uiManager.LeftButton.onClick.AddListener(OnClickLeft);
          _uiManager.RightButton.onClick.AddListener(OnClickRight);
          _uiManager.FullscreenButtonA.onClick.AddListener(OnClickScreen);
          _uiManager.FullscreenButtonB.onClick.AddListener(OnClickScreen);
          _uiManager.BgmSlider.onValueChanged.AddListener(OnBGMVolumeChange);
          _uiManager.SfxSlider.onValueChanged.AddListener(OnSFXVolumeChange);
          _uiManager.TitleButton.onClick.AddListener(ReturnToTitle);
          _uiManager.InitButton.onClick.AddListener(InitSettings);
          _uiManager.ReverButton.onClick.AddListener(RevertSettings);
        }

        public override void Enter()
        {
            base.Enter();
            _uiManager.SettingUI.SetActive(true);
            SetupSettings();
            LoadSliderSet();
        }
        public override void Exit()
        {
            base.Exit();
            _uiManager.SettingUI.SetActive(false);
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
            {   //스킬 UI로 이동
                //_uiStateMachine.ChangeState(_uiStateMachine.SkillUI);
            }
        }
        //초기 설정
        private void SetupSettings()
        {
            InitResolution();
            InitSettings();
        }

        private void ReturnToTitle()
        {
            SceneManager.LoadScene(StringNameSpace.Scenes.TitleScene);
        }

        //초기값 설정
        private void InitSettings()
        {
            _defaultResolutionIndex = _curResolutionIndex;
            _defaultFullscreen = _isFullScreen;
            _defaultBgmVolume = _bgmVolume.value;
            _defaultSfxVolume = _sfxVolume.value;
        }

        //설정 되돌리기
        private void RevertSettings()
        {
            _curResolutionIndex = _defaultResolutionIndex;
            _isFullScreen = _defaultFullscreen;
            SoundManager.Instance.SetVolume(SoundManager.SoundType.BGMMixer, _defaultBgmVolume);
            SoundManager.Instance.SetVolume(SoundManager.SoundType.SFXMixer, _defaultSfxVolume);
            ApplySettings();
            LoadSliderSet();
        }

        #region 설정 화면
        //해상도 설정
        private void InitResolution() // 가능한 해상도 불러오기
        {
            _resolutions = Screen.resolutions;
            
            var options = new List<string>();
          

            for (var i = 0; i < _resolutions.Length; i++)
            {
                var option = $"{_resolutions[i].width} x {_resolutions[i].height}";
                if (!options.Contains(option)) //중복 제거
                {
                    options.Add(option);
                }

                if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
                {
                    _curResolutionIndex = i;
                }
            }
            ApplySettings();
        }

        public void OnClickLeft()
        {
            _curResolutionIndex--;
            if (_curResolutionIndex < 0)
                _curResolutionIndex = _resolutions.Length - 1;
            ApplySettings();
        }
        public void OnClickRight()
        {
            _curResolutionIndex++;
            if (_curResolutionIndex >= _resolutions.Length)
                _curResolutionIndex = 0;
            ApplySettings();
        }
        //전체화면 설정
        public void OnClickScreen()
        {
            _isFullScreen = !_isFullScreen;
            ApplySettings();
        }

        //설정 화면
        public void ApplySettings()
        {
            Resolution res = _resolutions[_curResolutionIndex];
            Screen.SetResolution(res.width, res.height, _isFullScreen);
            _resolution.text = $"{res.width} x {res.height}";
            _fullscreen.text = _isFullScreen ? "전체화면" : "창모드";
            Debug.Log($"{_isFullScreen}{_fullscreen.text}");
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

          _bgmVolume.value = DbToLinear(bgmVolumeValue);
          _sfxVolume.value = DbToLinear(sfxVolumeValue);
        }

        private float DbToLinear(float db)
        {
            return Mathf.Pow(10f, db / 20f);
        }
        #endregion
}
