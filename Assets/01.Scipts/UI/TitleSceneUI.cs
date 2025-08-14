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
    [SerializeField] private Button bossRushButton;
    [SerializeField] private AudioClip startSound;

    
    private SettingUI _settingUI;
    private Coroutine _startGameCoroutine;

    private void Awake()
    {
        _settingUI = GetComponentInChildren<SettingUI>(true);
        newButton.onClick.AddListener(OnClickGameStart);
        LoadButton.onClick.AddListener(OnClickGameLoad);
        exitButton.onClick.AddListener(OnClickExit);
        settingButton.onClick.AddListener(OnClickSetting);
        bossRushButton.onClick.AddListener(OnClickBossRush);
    }

    private void Start()
    {
        _settingUI.TitleInit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _settingUI.Exit();
        }
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

    public void OnClickBossRush()
    {
        SceneManager.LoadScene(StringNameSpace.Scenes.BossRush);
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
