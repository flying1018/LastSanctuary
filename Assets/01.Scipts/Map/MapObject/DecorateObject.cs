using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorateObject : MonoBehaviour, IDamageable
{
    public bool isBroken;
    public int durability;

    public GameObject brokenPrefab;

    public void TakeDamage(int atk, DamageType type, Transform attackDir, float defpen)
    {
        if (isBroken) { return; }

        durability--;
        // 피격시 부셔지는 애니메이션 있다면 추가

        if (durability <= 0)
        {
            isBroken = true;
        }
        StartCoroutine(BreakObject());
    }

    private IEnumerator BreakObject()
    {
        isBroken = true;

        gameObject.SetActive(false);
        if (brokenPrefab != null)
        {
            GameObject brokenObj = Instantiate(brokenPrefab, transform.position, Quaternion.identity);

            Rigidbody2D[] fragments = brokenObj.GetComponentsInChildren<Rigidbody2D>();
            foreach (var rb in fragments)
            {
                Vector2 randomDir = Random.insideUnitCircle.normalized;
                rb.AddForce(randomDir * 2f, ForceMode2D.Impulse);
            }
        }

        yield return new WaitForSeconds(2f);

        brokenPrefab.SetActive(false);
    }
}
