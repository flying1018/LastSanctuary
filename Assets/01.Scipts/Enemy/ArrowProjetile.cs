using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : EnemyWeapon
{
  private Rigidbody2D _rigidbody2D;


  public void Init(int damage, float knockback)
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    Damage = damage;
    knockBackForce = knockback;
  }

  public void Shot(Vector2 dir, float arrowPower)
  {
    _rigidbody2D.velocity = Vector2.zero;
    _rigidbody2D.AddForce(dir * arrowPower, ForceMode2D.Impulse);
  }

  public override void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag(StringNameSpace.Tags.Ground) || (other.CompareTag(StringNameSpace.Tags.Player)))
    {
      gameObject.SetActive(false);
      ObjectPoolManager.Set((int)ObjectPoolManager.PoolingIndex.Arrow, gameObject, gameObject);
    }

    base.OnTriggerEnter2D(other);
  }
}
