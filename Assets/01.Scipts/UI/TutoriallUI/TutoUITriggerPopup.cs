using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutoUITriggerPopup : TutoUITriggerBase
{
    
    [SerializeField] private TextMeshPro titletext;
    [SerializeField] private TextMeshPro exptext;
    [SerializeField] private Sprite sprite;
    private bool _hasTriggeed = false;
    private TutorialUIPopup _uiPopup;
    
    private void Awake()
    {
        if (uiPrefab == null)
        {
            GameObject go = GameObject.Find("GuideUIPopup");
            if (go != null)
                uiPrefab = go;
        }
        _uiPopup = uiPrefab.GetComponentInChildren<TutorialUIPopup>(true);
    }
    
    protected override void  ShowUI()
    {
        if (_hasTriggeed) return;
        _hasTriggeed = true;
        _uiPopup.gameObject.SetActive(true);
        _uiPopup.Init(sprite, titletext.text, exptext.text);
    }
}
