using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(StringNameSpace.Tags.Player))
        {
            Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), _collider,true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(StringNameSpace.Tags.Player))
        {
            Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), _collider,false);
        }
    }
}
