using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button newButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private AudioClip startSound;
    
    private SettingUI _settingUI;
    private Coroutine _startGameCoroutine;

    private void Awake()
    {
        _settingUI = GetComponentInChildren<SettingUI>(true);
        _settingUI.TitleInit();
        
        newButton.onClick.AddListener(OnClickGameStart);
        exitButton.onClick.AddListener(OnClickExit);
        settingButton.onClick.AddListener(OnClickSetting);
    }

    public void OnClickGameStart()
    {
        if (_startGameCoroutine != null)
        {
            StopCoroutine(_startGameCoroutine);
            _startGameCoroutine = null;
        }
        _startGameCoroutine = StartCoroutine(StartGame_Coroutine());
    }

    private IEnumerator StartGame_Coroutine()
    {
        SoundManager.Instance.PlaySFX(startSound);
        yield return new WaitForSecondsRealtime(startSound.length/2);
        SceneManager.LoadScene(StringNameSpace.Scenes.RenewalTutorials);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickSetting()
    {
        _settingUI.Enter();
    }
    
}
