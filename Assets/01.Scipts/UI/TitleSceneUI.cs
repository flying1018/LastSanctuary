using System;
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
    
    private SettingUI _settingUI;

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
        //테스트용
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
