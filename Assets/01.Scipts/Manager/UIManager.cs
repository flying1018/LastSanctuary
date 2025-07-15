using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UIManagerSO data;
    
    [Header("UnifiedUI")]
    [SerializeField] private GameObject unifiedUI;
    [SerializeField] private Button exitButton;
    
    [Header("RelicUI")]
    [SerializeField] private GameObject relicUI;
    [SerializeField] private TextMeshProUGUI relicName;
    [SerializeField] private TextMeshProUGUI relicEffectText;
    [SerializeField] private TextMeshProUGUI relicDecsText;
    [SerializeField] private RectTransform statUIPivot;
    [SerializeField] private RectTransform equipUIPivot;
    [SerializeField] private RectTransform slotUIPivot;
    
    [Header("MainUI")]
    [SerializeField] private GameObject mainUI;
    [SerializeField] private RectTransform buffUIPivot;
    [SerializeField] private ConditionUI hpUI;
    [SerializeField] private ConditionUI staminaUI;
    [SerializeField] private Image potionIcon;
    [SerializeField] private TextMeshProUGUI potionText;
    
    
    public UIStateMachine StateMachine { get; set; }
    public PlayerCondition PlayerCondition { get; set; }
    public PlayerInventory PlayerInventory { get; set; }
    public PlayerInput PlayerInput { get; set; }
    public UIManagerSO Data { get => data;}
    
    //UnifiedUI
    public GameObject UnifiedUI { get => unifiedUI;}
    public Button ExitButton { get => exitButton;}
    
    //RelicUI
    public GameObject RelicUI { get => relicUI;}
    public TextMeshProUGUI RelicName { get => relicName;}
    public TextMeshProUGUI RelicEffectText { get => relicEffectText;}
    public TextMeshProUGUI RelicDecsText { get => relicDecsText;}
    public RectTransform StatUIPivot { get => statUIPivot;}
    public RectTransform EquipUIPivot { get => equipUIPivot;}
    public RectTransform SlotUIPivot { get => slotUIPivot;}
    //MainUI
    public GameObject MainUI { get => mainUI;}
    public RectTransform BuffUIPivot { get => buffUIPivot;}
    public Image PotionIcon { get => potionIcon;}
    public TextMeshProUGUI PotionText { get => potionText;}
    public ConditionUI HpUI { get => hpUI;}
    public ConditionUI StaminaUI { get => staminaUI;}


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
        
        StateMachine = new UIStateMachine(this);
    }

    public int ShowWarpUI(WarpObject slectWarpObj)
    {
        return 1;
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

    public GameObject InstantiateUI(GameObject prefab,RectTransform parent)
    {
        return Instantiate(prefab,parent);
    }

    public void UpdatePotionUI()
    {
        StateMachine.MainUI.UpdatePotionText();
    }

    public void UpdateBuffUI(StatObjectSO data)
    {
        StateMachine.MainUI.UpdateBuffUI(data);
    }

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

    #endregion
}
