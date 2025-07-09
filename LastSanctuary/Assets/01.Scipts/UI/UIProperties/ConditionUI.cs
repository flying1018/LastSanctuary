using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ConditionUI : MonoBehaviour
{
    public RectTransform rectTransform;
    public Slider slider;
    
    public void SetCurValue(float condition)
    {
        slider.value = condition;
    }
    public void SetMaxValue(float condition)
    {
        rectTransform.sizeDelta = new Vector2(condition, rectTransform.sizeDelta.y);
    }
}
