using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 메인 UI에 표시 되는 체력, 스태미나, 스킬 게이지
/// </summary>
[Serializable]
public class ConditionUI : MonoBehaviour
{
    public RectTransform rectTransform;
    public Slider slider;
    
    //현재 컨디션 설정
    public void SetCurValue(float condition)
    {
        slider.value = condition;
    }
    
    //최대 컨디션 설정
    public void SetMaxValue(float condition)
    {
        rectTransform.sizeDelta = new Vector2(condition, rectTransform.sizeDelta.y);
    }
}
