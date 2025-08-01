using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TUItype
{
    Repeat,
    Once,
}

public class TutorialUITrigger : MonoBehaviour
{
    [SerializeField] private GameObject uiPrefab;
    [SerializeField] private TUItype UItype;
    private bool hasTriggeed = false;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(StringNameSpace.Tags.Player)) return;
        ShowUI();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(StringNameSpace.Tags.Player)) return;
        if (UItype == TUItype.Repeat)
        {
            HideUI();
        }
    }
    
    public void ShowUI()
    {
        if (UItype == TUItype.Once)
        {
            if (hasTriggeed) return;
            hasTriggeed = true;
        }
        uiPrefab.SetActive(true);
    }

    public void HideUI()
    {
        uiPrefab.SetActive(false);
    }
}
    
