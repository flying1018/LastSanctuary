using TMPro;
using UnityEngine;


public class TutorialUIInterction : MonoBehaviour
{
   [SerializeField] private GameObject uiPrefab; 
   [SerializeField] private TextMeshPro titletext;
   [SerializeField] private TextMeshPro exptext;
   [SerializeField] private Sprite sprite;

   private bool _hasTriggeed = false;
   private TutorialUIPopup _uiPopup;

   private void Awake()
   {
      if (uiPrefab == null)
      {
         GameObject go = GameObject.Find("GuideUIPopup");
         if (go != null)
            uiPrefab = go;
      }

      _uiPopup = uiPrefab.GetComponentInChildren<TutorialUIPopup>(true);
   }

   public void ShowUI()
   {
      if (_hasTriggeed) return;
      _hasTriggeed = true;
      _uiPopup.gameObject.SetActive(true);
      _uiPopup.Init(sprite, titletext.text, exptext.text);
   }
}
