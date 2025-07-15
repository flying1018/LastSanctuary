using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTirrger : MonoBehaviour
{
    Boss _boss;
    
    public void Start()
    {
        _boss = FindAnyObjectByType<Boss>();
        _boss.gameObject.SetActive(false);
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(StringNameSpace.Tags.Player))
        {
            _boss.Target = other.gameObject.transform;
            _boss.gameObject.SetActive(true);
        }
    }
}
