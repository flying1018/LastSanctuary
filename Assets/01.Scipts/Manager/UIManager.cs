using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UIManagerSO data;

    public UIStateMachine StateMachine { get; private set; }
    public PlayerCondition PlayerCondition { get; private set; }
    public PlayerInventory PlayerInventory { get; private set; }
    public PlayerSkill PlayerSkill { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    public MainUI MainUI { get; private set; }
    public RelicUI RelicUI { get; private set; }
    public SettingUI SettingUI { get; private set; }
    public SkillUI SkillUI { get; private set; }
    public UIBaseState OffUI { get; private set; }
    public UIManagerSO Data { get => data; }
    public BossUI BossUI { get; set; }
    public ScreenFadeUI[] screenFadeUIs { get; set; }
    public SaveUI saveUI { get; set; }
    public ShowTextUI ShowTextUI { get; set; }
    public HintUI hintUI { get; private set; }
    public DeathUI DeathUI { get; set; }
    public TutorialUIPopup PopUpUI { get; set; }
    public Queue<TutorialUIPopup> PopUpQueue { get; set; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //testCode
        Init();
    }

    public void Init()
    {
        PlayerCondition = FindAnyObjectByType<PlayerCondition>();
        PlayerInventory = FindAnyObjectByType<PlayerInventory>();
        PlayerInput = FindAnyObjectByType<PlayerInput>();
        PlayerSkill = FindAnyObjectByType<PlayerSkill>();
        
        PopUpQueue = new Queue<TutorialUIPopup>();

        MainUI = GetComponentInChildren<MainUI>(true);
        RelicUI = GetComponentInChildren<RelicUI>(true);
        SettingUI = GetComponentInChildren<SettingUI>(true);
        SkillUI = GetComponentInChildren<SkillUI>(true);
        OffUI = GetComponentInChildren<UIBaseState>(true);

        screenFadeUIs = GetComponentsInChildren<ScreenFadeUI>(true);
        saveUI = GetComponentInChildren<SaveUI>(true);
        ShowTextUI = GetComponentInChildren<ShowTextUI>(true);
        DeathUI = GetComponentInChildren<DeathUI>(true);
        hintUI = GetComponentInChildren<HintUI>(true);

        StateMachine = new UIStateMachine(this);

        screenFadeUIs = GetComponentsInChildren<ScreenFadeUI>(true);
        saveUI = GetComponentInChildren<SaveUI>(true);
        PopUpUI = GetComponentInChildren<TutorialUIPopup>(true);

        BossUI = GetComponentInChildren<BossUI>(true);
        BossUI.Init();
    }


    private void Update()
    {
        StateMachine.HandleInput();
        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
        //Debug.Log(StateMachine.currentState);
    }

    #region Need MonoBehaviour Method

    //필요한 UI 프리펩 생성하는 메서드
    public GameObject InstantiateUI(GameObject prefab, RectTransform parent)
    {
        return Instantiate(prefab, parent);
    }

    //UI를 껏다켜는 메서드
    public void OnOffUI(bool isOn)
    {
        if(StateMachine == null) return;
        if (isOn)
        {
            StateMachine.ChangeState(StateMachine.MainUI);
        }
        else
        {
            StateMachine.ChangeState(StateMachine.OffUI);
        }
    }

    public void SetBossUI(bool isOn, BossCondition bossCondition = null)
    {
        if (isOn)
        {
            if (bossCondition == null) return;
            BossUI.BossCondition = bossCondition;
            BossUI.gameObject.SetActive(true);
        }
        else
        {
            BossUI.gameObject.SetActive(false);
        }
    }

    public void Fade(float duration = 1f, Color? color = null)
    {
        screenFadeUIs[0].gameObject.SetActive(true);
        screenFadeUIs[0].FadeBackground(duration);
    }

    public void FadeIn(int index, Color color, float startAlpha = 0f, float duration = 1f)
    {
        screenFadeUIs[index].gameObject.SetActive(true);
        screenFadeUIs[index].FadeIn(color, startAlpha, duration);
    }

    public void FadeOut(int index, Color color, float startAlpha = 1f, float duration = 1f)
    {
        screenFadeUIs[index].gameObject.SetActive(true);
        screenFadeUIs[index].FadeOut(color, startAlpha, duration);
    }


    public void SaveAnimation()
    {
        saveUI.gameObject.SetActive(true);
        saveUI.SaveAnima();
    }

    public void ShowItemText(string message, Vector3 worldPos)
    {
        var obj = Instantiate(data.itemTextUI, transform);
        obj.GetComponent<ItemTextUI>().ShowText(message, worldPos);
    }

    public void ShowText(float time)
    {
        ShowTextUI.gameObject.SetActive(true);
        ShowTextUI.ShowText(time);
    }

    public void DeathText(float time)
    {
        DeathUI.gameObject.SetActive(true);
        DeathUI.ShowText(time);
    }

    public void ShowHint(string body)
    {
        hintUI.gameObject.SetActive(true);
        hintUI.OpenHint(body);
    }

    public void CloseHint()
    {
        hintUI.CloseHint();
    }

    #endregion
}
