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
    [SerializeField] private Button relicUIButton;
    [SerializeField] private Button skillUIButton;
    [SerializeField] private Button settingUIButton;
    
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
    [SerializeField] private ConditionUI ultimateConditionUI;
    [SerializeField] private Image potionIcon;
    [SerializeField] private TextMeshProUGUI potionText;
    [SerializeField] private TextMeshProUGUI goldText;
    
    [Header("SettingUI")]
    [SerializeField] private GameObject settingUI;
    [SerializeField] private TextMeshProUGUI  resolutionText;
    [SerializeField] private TextMeshProUGUI  fullscreenText;
    [SerializeField] private Slider bgmVolume;
    [SerializeField] private Slider sfxVolume;
    //다른 UI에서는 필요없는거라 일단 빼놨습니다(의견 나눠봐야될듯)
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button fullscreenButtonA;
    [SerializeField] private Button fullscreenButtonB;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;


    public UIStateMachine StateMachine { get; set; }
    public PlayerCondition PlayerCondition { get; set; }
    public PlayerInventory PlayerInventory { get; set; }
    public PlayerInput PlayerInput { get; set; }
    public UIManagerSO Data { get => data;}
    
    //UnifiedUI
    public GameObject UnifiedUI { get => unifiedUI;}
    public Button ExitButton { get => exitButton;}
    public Button RelicUIButton { get => relicUIButton;}
    public Button SkillUIButton { get => skillUIButton;}
    public Button SettingUIButton { get => settingUIButton;}
    
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
    public TextMeshProUGUI GoldText { get => goldText;}
    public ConditionUI HpUI { get => hpUI;}
    public ConditionUI StaminaUI { get => staminaUI;}
    public ConditionUI UltimateConditionUI { get => ultimateConditionUI; }

    //SettingUI
    public GameObject SettingUI { get => settingUI;}
    public TextMeshProUGUI ResolutionText { get => resolutionText;}
    public TextMeshProUGUI FullscreenText { get => fullscreenText;}
    public Slider BgmVolume { get => bgmVolume;}
    public Slider SfxVolume { get => sfxVolume;}
    //임시
    public Button LeftButton { get => leftButton;}
    public Button RightButton { get => rightButton;}
    public Button FullscreenButtonA { get => fullscreenButtonA;}
    public Button FullscreenButtonB { get => fullscreenButtonB;}
    public Slider BgmSlider { get => bgmSlider;}
    public Slider SfxSlider { get => sfxSlider;}

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

    //필요한 UI 프리펩 생성하는 메서드
    public GameObject InstantiateUI(GameObject prefab,RectTransform parent)
    {
        return Instantiate(prefab,parent);
    }

    //포션 갯수 갱신
    public void UpdatePotionUI()
    {
        StateMachine.MainUI.UpdatePotionText();
    }

    //버프 UI 갱신
    public void UpdateBuffUI(StatObjectSO data)
    {
        StateMachine.MainUI.UpdateBuffUI(data);
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

    #endregion
}
