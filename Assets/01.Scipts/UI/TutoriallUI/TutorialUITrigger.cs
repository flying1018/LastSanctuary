using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UItype
{
    Repeat,
    Once,
}
public class TutorialUITrigger : MonoBehaviour
{
    [SerializeField] private GameObject uiPrefab;
    [SerializeField] private UItype UItype;
    [Header("Icon")]
    [SerializeField] private Transform uiPosition;
    [SerializeField] private List<TutorialUIAnim> uiAnims;
    [Header("Popup")]
    [SerializeField] private string message;
    [SerializeField] private Sprite image;
    
    private bool _hasTriggeed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(StringNameSpace.Tags.Player)) return;
        ShowUI();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(StringNameSpace.Tags.Player)) return;
        if (UItype == UItype.Repeat)
        {
            HideUI();
        }
    }
    
    public void ShowUI()
    {
        if (UItype == UItype.Once)
        {
            if (_hasTriggeed) return;
            _hasTriggeed = true;
            uiPrefab.SetActive(true);
        }
        else if (UItype == UItype.Repeat)
        {
            uiPrefab.transform.position = uiPosition.position;
            foreach (TutorialUIAnim uiAnim in uiAnims)
            {
                uiAnim.gameObject.SetActive(true);
                uiAnim.Init();
            }
        }
    }

    public void HideUI()
    {
        if (UItype == UItype.Repeat)
        {
            foreach (TutorialUIAnim uiAnim in uiAnims)
                uiAnim.gameObject.SetActive(false);
            return;
        }

        uiPrefab.SetActive(false);
    }
}
    
