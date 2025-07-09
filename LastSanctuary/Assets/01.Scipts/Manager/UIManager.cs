using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    #endregion
}
