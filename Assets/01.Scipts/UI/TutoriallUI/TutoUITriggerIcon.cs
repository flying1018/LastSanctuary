using System;
using System.Collections.Generic;
using UnityEngine;
public class TutoUITriggerIcon : TutoUITriggerBase
{
    
    [SerializeField] private Transform uiPosition;
    [SerializeField] private List<ObjectAnim> uiAnims;
 
    
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
        HideUI();
        uiPrefab.transform.position = uiPosition.position;
        foreach (ObjectAnim uiAnim in uiAnims)
        {
            uiAnim.gameObject.SetActive(true);
            uiAnim.Init();
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(StringNameSpace.Tags.Player)) return;
        HideUI();
    }

    public void HideUI()
    {
        foreach (ObjectAnim uiAnim in uiAnims)
            uiAnim.gameObject.SetActive(false);
    }
}
    
