using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : UnifiedUI
{
    private SellectSkillUI[] _sellectSkills;
    private SkillDescUI _skillDescUI;
    
    public SkillUI(UIStateMachine uiStateMachine) : base(uiStateMachine)
    {
        _sellectSkills = _uiManager.GetComponentsInChildren<SellectSkillUI>(true);
        _skillDescUI = _uiManager.GetComponentInChildren<SkillDescUI>(true);

        for (int i = 0; i < _sellectSkills.Length; i++)
        {
            int index = i;
            _sellectSkills[i].OnSelect += () => OnSelect(_sellectSkills[index]);
        }
        
    }

    public override void Enter()
    {
        base.Enter();

        _centerLine.localPosition = new Vector3(_data.centerLinePosition, 0, 0);
        _mouseLeftDesc.text = _data.skillLeftClickDesc;
        _mouseRightDesc.text = _data.sKillRightClickDesc;
        
        _uiManager.SkillUI.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        
        _uiManager.SkillUI.SetActive(false);
    }

    //좌클릭 시 실행
    private void OnSelect(SellectSkillUI sellect)
    {
        _skillDescUI.SetSkillDesc(sellect);
    }
    
    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyDown(KeyCode.E))
        {   //셋팅 UI로 이동
            _uiStateMachine.ChangeState(_uiStateMachine.SettingUI);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {   //성물 UI로 이동
            _uiStateMachine.ChangeState(_uiStateMachine.RelicUI);
        }
        
    }
}
