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
    [SerializeField] public TutorialUIManager tutoriaManager;
    [SerializeField] private int UIIndex;
    [SerializeField] TUItype UItype;
    private bool hasTriggeed = false;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (UItype == TUItype.Once)
        {
            if (hasTriggeed) return;
            hasTriggeed = true;
        }
        tutoriaManager.ShowUI(UIIndex, UItype);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (UItype == TUItype.Repeat)
        {
            tutoriaManager.HideUI();
        }
    }
    
}