using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PuzzleType
{
    Sequence,
    Stack,
}

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private PuzzleType puzzleType;
    [SerializeField] private List<GameObject> correctObj = new List<GameObject>();
    [SerializeField] private List<GameObject> diableObj = new List<GameObject>();
    
    private List<GameObject> _currentObj = new List<GameObject>();
    private int _currentIndex = 0;
    private bool _resetPuzzle = false;
    private bool _isFinished = false;
    
    public UnityEvent onSuccess;
    public UnityEvent onFail;

    public void OnCheckCorrect(GameObject obj)
    {
        if (_isFinished) return;
        if (puzzleType == PuzzleType.Sequence)
        {
            if (_currentObj.Contains(obj)) return;
        }

        _currentObj.Add(obj);

        if (_currentObj.Count == correctObj.Count)
        {
            if (IsSequenceCorrect())
            {
                _isFinished = true;
                onSuccess?.Invoke();
                
            }
            else
            {
                onFail?.Invoke();
                Invoke( nameof(ResetPuzzle), 0.5f);
                
            }
        }
    }
    
    //정답 확인
    public bool IsSequenceCorrect()
    {
        for (int i = 0; i < correctObj.Count; i++)
        {
            if (_currentObj[i] != correctObj[i])
                return false;
        }
        return true;
    }
    //리셋
    private void ResetPuzzle()
    {
        foreach (var obj in diableObj)
        {
            if (obj.TryGetComponent<PuzzleObject>(out var puzzleObject))
            {
               puzzleObject.Reset();
            }

            //if (obj != null) obj.SetActive(false);
        }
        _currentObj.Clear();
    }
}
