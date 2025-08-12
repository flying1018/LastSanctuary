using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossItemDropper : MonoBehaviour
{
   [Header("드롭 아이템")] 
   [SerializeField] private GameObject relicItems;
   [SerializeField] private List<GameObject> statItems;
   [Header("드롭 옵션")]
   [SerializeField] private Transform spawnPoint;
   [SerializeField] private float dropRange;
   [SerializeField] private float jumpHeight;
   [SerializeField] private float jumpDuration;
   [SerializeField] private Sprite circleSprite;
   //파티클
   [SerializeField] private GameObject highlight;
   //드롭될 아이템 설정
   public void DropItems()
   {
      Vector3 spawnPos = spawnPoint.position;
      List<(GameObject obj, bool isRelic)> dropItems = new List<(GameObject, bool)>();
      
      //렐릭
      dropItems.Add((relicItems, true));
      //스탯
      for (int i = 0; i < statItems.Count; i++)
      {
         dropItems.Add((statItems[i], false));
      }
      //드롭 위치
      float offsetX = -((dropItems.Count - 1) * dropRange) / 2f;

      for (int i = 0; i < dropItems.Count; i++)
      {
         Vector3 traget = new Vector3(offsetX + i * dropRange,0,0);
         Vector3 dropPos = spawnPos + traget;
         DropItem(dropItems[i].obj, spawnPos, dropPos, dropItems[i].isRelic);
      }
   }
   //하나씩 생성
   private void DropItem(GameObject prefab, Vector3 spawnPos, Vector3 dropPos, bool isRelic)
   {
      GameObject item = ObjectPoolManager.Get(prefab, (int)PoolingIndex.Item);
      item.transform.position = spawnPos;

      if (isRelic)
      {
         SpriteRenderer sprite = item.GetComponent<SpriteRenderer>();
         sprite.sprite = circleSprite;
         sprite.color = new Color(1f,1f,0,0.5f);
         GameObject effect = Instantiate(highlight, item.transform);
         effect.transform.localPosition = Vector3.zero;
      }

      StartCoroutine(DropEvent_Coroutine(item, spawnPos, dropPos, isRelic));
   }
   //아이템 분수 연출
   private IEnumerator DropEvent_Coroutine(GameObject item, Vector3 spawnPos, Vector3 dropPos, bool isRelic)
   {
      float time = 0f;

      while (time < jumpDuration)
      {
         float t = time / jumpDuration;
         float y = -4f * jumpHeight * Mathf.Pow(t - 0.5f, 2) + jumpHeight;

         item.transform.position = Vector3.Lerp(spawnPos, dropPos, t) + Vector3.up * y;

         time += Time.deltaTime;
         yield return null;
      }
      item.transform.position = dropPos;
      
      if (isRelic)
      {
         CollectObject obj = item.GetComponent<CollectObject>();
         SpriteRenderer sprite = item.GetComponent<SpriteRenderer>();
         sprite.sprite = obj.Data.relicSprite;
         sprite.color = Color.white;
         obj.PlayDropSound();
      }
   }
}
