using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
  private Vector2 _direction;
  private int _attack;
  private float _arrowSpeed;
  private float _knockBackForce;

  private void Update()
  {
    transform.Translate(_direction * _arrowSpeed * Time.deltaTime);
  }

  public void Init(Vector2 dir, int attack, float arrowSpeed, float knockBackForce)
  {
    _direction = dir;
    _attack = attack;
    _arrowSpeed = arrowSpeed;
    _knockBackForce = knockBackForce;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag(StringNameSpace.Tags.AerialPlatform))
    {
      gameObject.SetActive(false);
      return;
    }

    if (other.TryGetComponent(out IDamageable idamageable))
    {
      if(other.gameObject.CompareTag(StringNameSpace.Tags.Enemy)) return;
      Debug.Log(idamageable + "는 " + _attack +"의 를 받았다.");
      idamageable.TakeDamage(_attack, DamageType.Attack,this.transform,_knockBackForce);
    }
  }
}
