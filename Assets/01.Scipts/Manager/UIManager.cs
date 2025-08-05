using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public ScreenFadeUI screenFadeUI { get; set; }
    public SaveUI saveUI { get; set; }


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

        MainUI = GetComponentInChildren<MainUI>(true);
        RelicUI = GetComponentInChildren<RelicUI>(true);
        SettingUI = GetComponentInChildren<SettingUI>(true);
        SkillUI = GetComponentInChildren<SkillUI>(true);
        OffUI = GetComponentInChildren<UIBaseState>(true);

        screenFadeUI = GetComponentInChildren<ScreenFadeUI>(true);
        saveUI = GetComponentInChildren<SaveUI>(true);

        StateMachine = new UIStateMachine(this);

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

    public void FadeIn(float duration = 1f)
    {
        DebugHelper.Log("FadeIn실행");
        StartCoroutine(screenFadeUI.FadeIn_Coroutine(duration));
    }

    public void FadeOut(float duration = 1f, Color? color = null)
    {
        DebugHelper.Log("FadeOut실행");
        StartCoroutine(screenFadeUI.FadeOut_Coroutine(duration, color));
    }

    #endregion
}
