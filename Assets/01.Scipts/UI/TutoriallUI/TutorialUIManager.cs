using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TutorialUIManager : MonoBehaviour
{ 
    [SerializeField] private GameObject UIGuideIcon;
    [SerializeField] private Image uiIcon;
    [SerializeField] private GameObject UIGuideImage;
    [SerializeField] private Image uiImage;
    //UI 스프라이트 및 위치
    [SerializeField] private List<Sprite> uiSprites;
    [SerializeField] private List<Transform> uiTransforms;
    [SerializeField] private Transform uiParent;
    private void Awake()
    {
        uiTransforms.Clear();
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
        
      Sprite sprite = uiSprites[index];

      if (type == TUItype.Repeat)
      {
          uiIcon.sprite = sprite;
          if (index < uiTransforms.Count && uiTransforms[index] != null)
          {
              UIGuideIcon.transform.position = uiTransforms[index].position;
          }
          UIGuideIcon.SetActive(true);
      }
      else if (type == TUItype.Once)
      {
          uiImage.sprite = sprite;
          UIGuideImage.SetActive(true);
      }
    }

    public void HideUI()
    {
        UIGuideIcon.SetActive(false);
        UIGuideImage.SetActive(false);
    }
}
