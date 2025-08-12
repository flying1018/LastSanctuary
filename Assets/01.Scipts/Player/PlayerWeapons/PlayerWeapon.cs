using UnityEngine;

//플레이어의 무기
public class PlayerWeapon : Weapon
{
    [SerializeField] protected AudioClip sound;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player)) return;

        base.OnTriggerEnter2D(other);

        if (other.TryGetComponent(out Condition condition))
        {
            condition.DamageDelay();
        }

        if (other.CompareTag(StringNameSpace.Tags.Enemy))
        {
            if (WeaponInfo.Condition is PlayerCondition dummy)
            {
                dummy.CurUltimate += WeaponInfo.UltimateValue;
            }
        }
    }
  
    
   
}
