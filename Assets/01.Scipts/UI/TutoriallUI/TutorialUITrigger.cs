using System;
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
    [SerializeField] private Transform uiPosition;
    
    [SerializeField] private string animationNameToPlay;
    private Animator _animator;
    private bool _hasTriggeed = false;


    private void Awake()
    {
        _animator = uiPrefab.GetComponentInChildren<Animator>(true);
    }

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
            if (_hasTriggeed) return;
            _hasTriggeed = true;
        }
        else if (UItype == TUItype.Repeat)
        {
            uiPrefab.transform.position = uiPosition.position;
        }

        uiPrefab.SetActive(true);
        _animator.Play(animationNameToPlay);
    }

    public void HideUI()
    {
        uiPrefab.SetActive(false);
    }
}
    
