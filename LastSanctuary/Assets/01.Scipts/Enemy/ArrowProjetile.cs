using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : EnemyWeapon
{
  private Rigidbody2D _rigidbody2D;
  private GameObject _prefab;
  
  public void Init(GameObject data)
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _prefab = data;
  }

  public void Shot(Vector2 dir, float arrowPower)
  {
    _rigidbody2D.AddForce(dir * arrowPower, ForceMode2D.Impulse);
  }

  public override void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag(StringNameSpace.Tags.Ground))
    {
      gameObject.SetActive(false);
      ObjectPoolManager.Set((int)ObjectPoolManager.PoolingIndex.Arrow, _prefab, gameObject);
      return;
    }
   base.OnTriggerEnter2D(other);
   if (other.CompareTag(StringNameSpace.Tags.Player))
   {
     gameObject.SetActive(false);
     ObjectPoolManager.Set((int)ObjectPoolManager.PoolingIndex.Arrow, gameObject, gameObject);
   }
  }
}
