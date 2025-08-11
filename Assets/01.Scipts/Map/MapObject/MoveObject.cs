using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{

    protected bool _isTurnOn;

    public virtual void MoveObj()
    {

    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
