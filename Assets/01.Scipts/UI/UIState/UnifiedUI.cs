using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnifiedUI : UIBaseState
{
    [Header("UnifiedUI")] [SerializeField] protected GameObject unifiedUI;
    [SerializeField] protected Button exitButton;
    [SerializeField] protected Button relicUIButton;
    [SerializeField] protected Button skillUIButton;
    [SerializeField] protected Button settingUIButton;
    [SerializeField] protected GameObject mouseLeft;
    [SerializeField] protected GameObject mouseRight;
    [SerializeField] protected RectTransform centerLinePos;

    protected TextMeshProUGUI _mouseLeftDesc;
    protected TextMeshProUGUI _mouseRightDesc;

    public UnifiedUI(UIStateMachine uiStateMachine) : base(uiStateMachine)
    {
    }

    public override void Init(UIStateMachine uiStateMachine)
    {
        base.Init(uiStateMachine);

        _mouseLeftDesc = mouseLeft.GetComponentInChildren<TextMeshProUGUI>();
        _mouseRightDesc = mouseRight.GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void Enter()
    {
        Debug.Log(_uiManager);
        
        //타이틀 씬이 아닐때
        if (_uiManager)
        {
            relicUIButton.onClick.AddListener(OnClickRelicUIButton);
            skillUIButton.onClick.AddListener(OnClickSkillUIButton);
            settingUIButton.onClick.AddListener(OnClickSettingUIButton);
            _uiManager.PlayerInput.enabled = false;
        }

        exitButton.onClick.AddListener(OnClickExitButton);
        centerLinePos.localPosition = Vector3.zero;
        unifiedUI.SetActive(true);
    }

    public override void Exit()
    {

        //타이틀 씬이 아닐때
        if (_uiManager != null)
        {
            relicUIButton.onClick.RemoveAllListeners();
            skillUIButton.onClick.RemoveAllListeners();
            settingUIButton.onClick.RemoveAllListeners();
            _uiManager.PlayerInput.enabled = true;
        }

        exitButton.onClick.RemoveAllListeners();
        unifiedUI.SetActive(false);
    }

    //성물 버튼
    public void OnClickRelicUIButton()
    {
        _uiStateMachine.ChangeState(_uiStateMachine.RelicUI);
    }

    //스킬 버튼
    public void OnClickSkillUIButton()
    {
        _uiStateMachine.ChangeState(_uiStateMachine.SkillUI);
    }

    //설정 버튼
    public void OnClickSettingUIButton()
    {
        _uiStateMachine.ChangeState(_uiStateMachine.SettingUI);
    }

    //나가기 버튼
    public void OnClickExitButton()
    {
        if (_uiManager != null)
            _uiStateMachine.ChangeState(_uiStateMachine.MainUI);
        else
            Exit();
    }

    public override void HandleInput()
    {
        //Esc 입력 시
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //메인 UI로 이동
            _uiStateMachine.ChangeState(_uiStateMachine.MainUI);
        }
    }
}