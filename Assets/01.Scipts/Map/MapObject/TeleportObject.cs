using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform target;
    private Player _player;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            if (other.TryGetComponent(out Player player))
            {
                _player = player;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            _player = null;
        }
    }

    public void Interact()
    {
       //이동
       _player.transform.position = target.position;
    }
}
