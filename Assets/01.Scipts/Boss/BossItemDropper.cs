using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossItemDropper : MonoBehaviour
{
   [Header("드롭 아이템")] 
   [SerializeField] private List<GameObject> relicItems;
   [SerializeField] private List<GameObject> statItems;
   [Header("드롭 옵션")]
   [SerializeField] private float dropRange;

   public void DropItems()
   {
      int rand = Random.Range(0, relicItems.Count + 1);

      for (int i = 0; i < relicItems.Count; i++)
      {
         int index = Random.Range(0, relicItems.Count);
         Vector3 offset = new Vector3(Random.Range(-dropRange, dropRange), 0, Random.Range(-dropRange, dropRange));
        // GameObject randomItem = ObjectPoolManager.Get();
      }
   }
}
