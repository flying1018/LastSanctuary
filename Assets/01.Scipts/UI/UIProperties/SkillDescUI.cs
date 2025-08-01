using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillDescUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI controlKeys;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillOpenConditions;
    [SerializeField] private TextMeshProUGUI needGold;
    
    public void SetSkillDesc(SellectSkillUI skillUI)
    {
        skillName.text = skillUI.SkillName;
        controlKeys.text = skillUI.ControlKeys;
        skillDescription.text = skillUI.SkillDescription;
        skillOpenConditions.text = skillUI.SkillOpenConditions;
        needGold.text = skillUI.NeedGold + "G";
    }
    
    
}
