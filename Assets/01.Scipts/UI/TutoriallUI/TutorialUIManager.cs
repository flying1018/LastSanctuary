using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIManager : MonoBehaviour
{
    [SerializeField] private GameObject uiBackground;
    [SerializeField] private Image uiImage;
    
    [SerializeField] private List<Sprite> uiSprites;

    public void ShowUI(int index)
    {
      uiImage.sprite = uiSprites[index];
      uiBackground.SetActive(true);
    }

    public void HideUI()
    {
        uiBackground.SetActive(false);
    }
}
