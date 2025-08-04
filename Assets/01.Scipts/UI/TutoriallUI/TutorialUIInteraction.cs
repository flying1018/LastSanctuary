using UnityEngine;


public class TutorialUIInterction : MonoBehaviour
{
   [SerializeField] private GameObject uiPrefab;
   private bool _hasTriggeed = false;

   public void ShowUI()
   {
      if (_hasTriggeed) return;
      _hasTriggeed = true;
      uiPrefab.SetActive(true);
   }

   public void HideUI()
   {
      uiPrefab.SetActive(false);
   }
}
