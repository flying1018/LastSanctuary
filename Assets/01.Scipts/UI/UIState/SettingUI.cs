using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : UnifiedUI
{
        private TextMeshProUGUI  _resolution;
        private TextMeshProUGUI  _fullscreen;
        private Slider _bgmVolume;
        private Slider _sfxVolume;
    
        private Resolution[] _resolutions;
        private int _curResolutionIndex = 0;
        private bool _isFullScreen = true;

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

        private void SetupSettings()
        {
            InitResolution();
            
        }

        
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
            _fullscreen.text = $"{Screen.fullScreen}";
        }
        
        public void OnBGMVolumeChange(float value)
        {
            SoundManager.Instance.SetVolume(SoundManager.SoundType.BGMVolume, value);
            Debug.Log($"배경음 볼륨: {value:F2}");
        }
        
        
        public void OnSFXVolumeChange(float value)
        {
            SoundManager.Instance.SetVolume(SoundManager.SoundType.SFXVolume, value);
            Debug.Log($"효과음 볼륨: {value:F2}");
        }
        
        
        private void LoadSliderSet()
        {
          float bgmVolumeValue, sfxVolumeValue;
          
          SoundManager.Instance.mixer.GetFloat(SoundManager.SoundType.BGMVolume.ToString(), out bgmVolumeValue);
          SoundManager.Instance.mixer.GetFloat(SoundManager.SoundType.SFXVolume.ToString(), out sfxVolumeValue);
          
          _bgmVolume.value = bgmVolumeValue;
          _sfxVolume.value = sfxVolumeValue;
        }
}
