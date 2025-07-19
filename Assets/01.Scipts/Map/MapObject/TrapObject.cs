using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private Transform returnPostion;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable idamageable)) { return; }

        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            idamageable.TakeDamage(damage, DamageType.Attack);

            StartCoroutine(ReturnPlayer(other.transform));
        }
    }

    public IEnumerator ReturnPlayer(Transform playerTrans)
    {
        yield return new WaitForSeconds(1f);
        playerTrans.position = returnPostion.position;
    }
}
