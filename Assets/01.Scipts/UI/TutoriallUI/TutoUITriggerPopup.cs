using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutoUITriggerPopup : TutoUITriggerBase
{
    
    
    [SerializeField] private string titletext;
    [SerializeField] private string exptext;
    [SerializeField] private Sprite sprite;
    private bool _hasTriggeed = false;
    private TutorialUIPopup uiPopup;
    
    private void Awake()
    {
        if (uiPrefab == null)
        {
            GameObject go = GameObject.Find("Popup");
            if (go != null)
                uiPrefab = go;
        }
        uiPopup = uiPrefab.GetComponent<TutorialUIPopup>();
    }
    
    protected override void  ShowUI()
    {
        if (_hasTriggeed) return;
        _hasTriggeed = true;
        uiPrefab.SetActive(true);
        uiPopup.Init(sprite, titletext, exptext);

    }

    protected override void HideUI()
    {
        
    }
}
