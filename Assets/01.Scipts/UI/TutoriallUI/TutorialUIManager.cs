using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TutorialUIManager : MonoBehaviour
{ 
    //UI 프리펩 및 위치
    [SerializeField] private List<GameObject> uiPrefabs;
    [SerializeField] private List<Transform> uiTransforms;
    [SerializeField] private GameObject tGuideUI;
    [SerializeField] private Transform uiParent;

    private GameObject curUI;
    
    private void Awake()
    {
        uiTransforms.Clear();
        uiPrefabs.Clear();
        
        foreach (Transform guide in tGuideUI.transform)
        {
            uiPrefabs.Add(guide.gameObject);
        }
        foreach (Transform trigger in uiParent)
        {
            if(trigger.childCount > 0)
            {
                uiTransforms.Add(trigger.GetChild(0));
            }
        }
    }

    public void ShowUI(int index, TUItype type)
    {
        HideUI();
        
      curUI = uiPrefabs[index];

      if (type == TUItype.Repeat)
      {
          if (index < uiTransforms.Count && uiTransforms[index] != null)
          {
               curUI.transform.position = uiTransforms[index].position;
          }
      }
      else if (type == TUItype.Once)
      {
         curUI.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
      }
      curUI.SetActive(true);
    }

    public void HideUI()
    {
        if (curUI != null)
        {
            curUI.SetActive(false);
            curUI = null;
        }
    }
}
