using System.Collections;
using UnityEngine;

public class LaserBarrier : MoveObject
{
    [SerializeField] private Sprite barrierSprite;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D col;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        spriteRenderer.sprite = barrierSprite;
        SetBarrierActive(true);
    }

    public override void MoveObj()
    {
        _isTurnOn = !_isTurnOn;
        SetBarrierActive(_isTurnOn);
    }

    private void SetBarrierActive(bool isActive)
    {
        col.enabled = isActive;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!col.enabled) return;
        if (other.CompareTag("Player"))
        {
            // other.GetComponent<PlayerCondition>().TakeDamage();
        }
    }
}
