using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUI : ShowTextUI
{
    [SerializeField] protected string defaultText = "";
    
    public override void ShowText(float totalSeconds, string text = null)
    {
        if (_routine != null) StopCoroutine(_routine);
        _routine = StartCoroutine(ShowText_Coroutine(totalSeconds, defaultText));
    }
}
