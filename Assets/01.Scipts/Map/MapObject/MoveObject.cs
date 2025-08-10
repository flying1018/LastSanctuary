using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public enum MoveObjType
    {
        None,
        IronCage,
        Rope,
        LaserBarrier,
        Puzzle,
        BossDoor,
    }

    [SerializeField] protected MoveObjType moveObjType;

    protected bool _isTurnOn;

    public virtual void MoveObj()
    {

    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
