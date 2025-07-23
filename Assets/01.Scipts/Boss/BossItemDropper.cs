using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossItemDropper : MonoBehaviour
{
   [Header("드롭 아이템")] 
   [SerializeField] private List<GameObject> relicItems;
   [SerializeField] private List<GameObject> statItems;
   [Header("드롭 옵션")]
   [SerializeField] private Transform spawnPoint;
   [SerializeField] private float dropRange;
   [SerializeField] private float jumpHeight;
   [SerializeField] private float jumpDuration;
   [SerializeField] private int minDrop;
   [SerializeField] private int maxDrop;

   //드롭될 아이템 설정
   public void DropItems()
   {
      Vector3 spawnPos = spawnPoint.position;
      List<GameObject> dropItems = new List<GameObject>();
      
      //렐릭
      if (relicItems.Count > 0)
      {
         int relicIndex = Random.Range(0, relicItems.Count);
         dropItems.Add(relicItems[relicIndex]);
      }
      //스탯
      int dropCount = Random.Range(minDrop, maxDrop+1);
      for (int i = 0; i < dropCount; i++)
      {
         int statIndex = Random.Range(0, statItems.Count);
         dropItems.Add(statItems[statIndex]);
      }
      //드롭 위치
      float offsetX = -((dropItems.Count - 1) * dropRange) / 2f;

      for (int i = 0; i < dropItems.Count; i++)
      {
         Vector3 traget = new Vector3(offsetX + i * dropRange,0,0);
         Vector3 dropPos = spawnPos + traget;
         DropItem(dropItems[i], spawnPos, dropPos);
      }
   }
   //하나씩 생성
   private void DropItem(GameObject prefab, Vector3 spawnPos, Vector3 dropPos)
   {
      GameObject item = ObjectPoolManager.Get(prefab, (int)ObjectPoolManager.PoolingIndex.Item);
      item.transform.position = spawnPos;
      StartCoroutine(DropEvent_Coroutine(item, spawnPos, dropPos));
   }
   //아이템 분수 연출
   private IEnumerator DropEvent_Coroutine(GameObject item, Vector3 spawnPos, Vector3 dropPos)
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
   }
}
