using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour
{
    //필드
    public WeaponInfo WeaponInfo;
    
    //직렬화
    [SerializeField] private int damage;
    [SerializeField] private Transform returnPostion;
    [SerializeField] private float returnTime;

    private void Awake()
    {
        WeaponInfo = new WeaponInfo();
        WeaponInfo.Attack = damage;
        WeaponInfo.DamageType = DamageType.Trap;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable idamageable)) { return; }

        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            idamageable.TakeDamage(WeaponInfo);
            StartCoroutine(ReturnPlayer(other.transform));
        }
    }

    public IEnumerator ReturnPlayer(Transform playerTrans)
    {
        yield return new WaitForSeconds(returnTime);
        playerTrans.position = returnPostion.position;
    }
}
