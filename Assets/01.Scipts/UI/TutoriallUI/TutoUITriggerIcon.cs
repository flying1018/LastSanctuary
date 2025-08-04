using System;
using System.Collections.Generic;
using UnityEngine;
public class TutoUITriggerIcon : TutoUITriggerBase
{
    
    [SerializeField] private Transform uiPosition;
    [SerializeField] private List<TutorialUIAnim> uiAnims;
 
    
    private bool _hasTriggeed = false;

    private void Awake()
    {
        if (uiPrefab == null)
        {
            GameObject go = GameObject.Find("GuideUIIcon");
            if (go != null)
                uiPrefab = go;
        }
    }

    protected override void  ShowUI()
    {
        uiPrefab.transform.position = uiPosition.position;
        foreach (TutorialUIAnim uiAnim in uiAnims)
        {
            uiAnim.gameObject.SetActive(true);
            uiAnim.Init();
        }

    }

    protected override void HideUI()
    {
        foreach (TutorialUIAnim uiAnim in uiAnims)
            uiAnim.gameObject.SetActive(false);
    }
}
    
