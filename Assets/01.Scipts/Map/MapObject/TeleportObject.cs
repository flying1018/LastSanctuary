using UnityEngine;

public enum TeleportType
{
    Auto,
    Interactable,
}

public class TeleportObject : MonoBehaviour, IInteractable
{  
    [SerializeField] private TeleportType teleportType;
    [SerializeField] private Transform target;
    
    [SerializeField] private float cooltime = 0.5f;
    private static bool _istelpoCooldown;
    
    private Player _player;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(StringNameSpace.Tags.Player)) return;

        if (other.TryGetComponent(out Player player))
        {
            _player = player;

            if (teleportType == TeleportType.Auto && !_istelpoCooldown)
            {
                Teleport(_player);
                _istelpoCooldown = true;
                Invoke(nameof(ResetCooldown), cooltime);
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
        if(teleportType == TeleportType.Interactable && _player != null)
            Teleport(_player);
    }
    
    private void ResetCooldown()
    {
        _istelpoCooldown = false;
    }
    
    private void Teleport(Player player)
    {
        if (_player != null)
        {
            player.transform.position = target.position;
        }
    }
}
