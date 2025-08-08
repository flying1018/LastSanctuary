using System.Collections;
using UnityEngine;

public class LaserBarrier : MoveObject
{
    [SerializeField] private Sprite barrierSprite;
    [SerializeField] private int damage;
    [SerializeField] private int knockBackPower;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D col;

    public WeaponInfo WeaponInfo;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        spriteRenderer.sprite = barrierSprite;
        SetBarrierActive(true);
        WeaponInfo = new WeaponInfo();
        WeaponInfo.Attack = damage;
        WeaponInfo.KnockBackForce = knockBackPower;
        WeaponInfo.DamageType = DamageType.Attack;
    }

    public override void MoveObj()
    {
        _isTurnOn = !_isTurnOn;
        SetBarrierActive(_isTurnOn);
    }

    private void SetBarrierActive(bool isActive)
    {
        col.enabled = isActive;
        spriteRenderer.enabled = isActive;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!col.enabled) return;
        
        if (other.TryGetComponent(out PlayerCondition playerCondition))
        {
            playerCondition.TakeDamage(WeaponInfo);
            Debug.Log("a");
            playerCondition.ApplyKnockBack(WeaponInfo, transform);
        }
    }
}
