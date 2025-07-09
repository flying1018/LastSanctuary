using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UIManagerSO data;
    
    [Header("RelicUI")]
    [SerializeField] private TextMeshProUGUI relicName;
    [SerializeField] private TextMeshProUGUI relicEffectText;
    [SerializeField] private TextMeshProUGUI relicDecsText;
    [SerializeField] private RectTransform statUIPivot;
    [SerializeField] private RectTransform equipUIPivot;
    [SerializeField] private RectTransform slotUIPivot;
    
    [Header("MainUI")]
    [SerializeField] private RectTransform buffUIPivot;
    [SerializeField] private ConditionUI hpUI;
    [SerializeField] private ConditionUI staminaUI;
    [SerializeField] private Image potionIcon;
    [SerializeField] private TextMeshProUGUI potionText;
    
    
    public UIStateMachine UiStateMachine { get; set; }
    public PlayerCondition PlayerCondition { get; set; }
    public PlayerInventory PlayerInventory { get; set; }
    public UIManagerSO Data { get => data;}
    
    //RelicUI
    public TextMeshProUGUI RelicName { get => relicName;}
    public TextMeshProUGUI RelicEffectText { get => relicEffectText;}
    public TextMeshProUGUI RelicDecsText { get => relicDecsText;}
    public RectTransform StatUIPivot { get => statUIPivot;}
    public RectTransform EquipUIPivot { get => equipUIPivot;}
    public RectTransform SlotUIPivot { get => slotUIPivot;}
    //MainUI
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
        
        UiStateMachine = new UIStateMachine(this);
    }

    public int ShowWarpUI(WarpObject slectWarpObj)
    {
        return 1;
    }

    private void Update()
    {
        UiStateMachine.HandleInput();
        UiStateMachine.Update();
    }

    private void FixedUpdate()
    {
        UiStateMachine.PhysicsUpdate();
    }

    #region Need MonoBehaviour Method

    public GameObject InstantiateUI(GameObject prefab,RectTransform parent)
    {
        return Instantiate(prefab,parent);
    }

    public void UpdatePotionUI()
    {
        UiStateMachine.MainUI.UpdatePotionText();
    }

    public void UpdateBuffUI(StatObjectSO data)
    {
        UiStateMachine.MainUI.UpdateBuffUI(data);
    }

    #endregion
}
