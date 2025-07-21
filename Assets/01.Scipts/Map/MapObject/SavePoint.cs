using System.Collections;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    private int _index;
    private bool _isInteracted;

    [Header("interact Animation")]
    [SerializeField] private GameObject rope;
    [SerializeField] private float enterTime;
    [SerializeField] private GameObject bell;
    [SerializeField] private float ropePosition;
    [SerializeField] private float bellRotateAngle;
    [SerializeField] private float bellRotateSpeed;
    [SerializeField] private SpriteRenderer effect;
    [SerializeField] private float targetAlpha;

    public void Interact()
    {
        if (_isInteracted) { return; }

        SaveManager.Instance.SetSavePoint(this.transform.position);
        ItemManager.Instance.playerCondition.PlayerRecovery(); // 회복
        ItemManager.Instance.playerInventory.SupplyPotion();
        MapManager.Instance.RespawnEnemies();
        //MapManager.Instance.RespawnItems();

        StartCoroutine(Save_Coroutine());
    }

    private IEnumerator Save_Coroutine()
    {
        yield return new WaitForSeconds(enterTime);
        
        while (rope.transform.localPosition.y > ropePosition)
        {
            rope.transform.position += Vector3.down * Time.deltaTime;
            yield return null;
        }
        
        while (rope.transform.localPosition.y < 0)
        {
            rope.transform.position += Vector3.up * Time.deltaTime;
            yield return null;
        }

        StartCoroutine(ShakeBell_Coroutine());
        StartCoroutine(Effect_Coroutine());
    }

    IEnumerator Effect_Coroutine()
    {
        Color color = effect.color;
        
        while(targetAlpha > effect.color.a)
        {
            color.a += Time.deltaTime * targetAlpha;
            effect.color = color;
            yield return null;
        }

        while (effect.color.a != 0)
        {
            color.a -= Time.deltaTime * targetAlpha;
            effect.color = color;
            yield return null;
        }
    }

    IEnumerator ShakeBell_Coroutine()
    {
        while (bell.transform.rotation.z < Quaternion.Euler(0,0,bellRotateAngle).z)
        {
            RotateBell(bellRotateAngle, bellRotateSpeed);
            yield return null;       
        }

        while (bell.transform.rotation.z > Quaternion.Euler(0,0,-bellRotateAngle).z)
        {
            RotateBell(-bellRotateAngle, bellRotateSpeed);
            yield return null;
        }
        
        while (bell.transform.rotation.z < Quaternion.Euler(0,0,bellRotateAngle).z)
        {
            RotateBell(bellRotateAngle, bellRotateSpeed);
            yield return null;       
        }
        
        while (bell.transform.rotation.z > Quaternion.Euler(0,0,0).z)
        {
            RotateBell(-bellRotateAngle, bellRotateSpeed);
            yield return null;
        }
    }

    private void RotateBell(float angle, float speed)
    {
        bell.transform.rotation *= Quaternion.Euler(0, 0, angle * Time.deltaTime * speed);
    }
}