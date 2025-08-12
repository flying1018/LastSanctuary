using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button newButton;
    [SerializeField] private Button LoadButton;
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
        LoadButton.onClick.AddListener(OnClickGameLoad);
        exitButton.onClick.AddListener(OnClickExit);
        settingButton.onClick.AddListener(OnClickSetting);
    }

    public async void OnClickGameStart()
    {
        // SoundManager.Instance.PlaySFX(startSound);
        // await Task.Delay((int)startSound.length * 500);
        SceneManager.LoadScene(StringNameSpace.Scenes.RenewalTutorials);
    }
    
    public void OnClickGameLoad()
    {
        SceneManager.LoadScene(StringNameSpace.Scenes.SancScene);
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
