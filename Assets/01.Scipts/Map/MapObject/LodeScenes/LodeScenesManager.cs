using UnityEngine;
using UnityEngine.SceneManagement;

public class LodeScenesManager : Singleton<LodeScenesManager>
{
   public string portalUid;
   private PortalType? nextPortalType;

   private void Awake()
   {
      base.Awake();
      DontDestroyOnLoad(this);
      SceneManager.sceneLoaded += OnSceneLoaded;
   }

   private void OnDestroy()
   {
      SceneManager.sceneLoaded -= OnSceneLoaded;
   }

   private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
   {
      SetSpawnPoint();
   }

   private void SetSpawnPoint()
   {
      var portals = FindObjectsOfType<LodeScenePortal>();
      var player = FindObjectOfType<Player>();
      
      if(nextPortalType == PortalType.Lobby)
      {
         foreach (var portal in portals)
         {
            if (portal.portalType == PortalType.Lobby)
            {
               player.transform.position = portal.spawnPoint.transform.position;
               ShowText(portal);
               break;
            }
         }
      }
      else
      {
         foreach (var portal in portals)
         {
            if (portal.uid == portalUid)
            {
               player.transform.position = portal.spawnPoint.transform.position;
               ShowText(portal);
               break;
            }
         }
      }
   }

   public void ShowText(LodeScenePortal portal)
   {
      var areaInfo = portal.GetComponent<AreaInfo>();
      if (areaInfo != null)
      {
         UIManager.Instance.ShowTextUI.gameObject.SetActive(true);
         UIManager.Instance.ShowTextUI.ShowText(2f, areaInfo.AreaName);
      }
   }
   public void LoadScenePortal(LodeScenePortal potal)
   {
      if (potal.portalType == PortalType.Map)
      {
         portalUid = potal.uid;
         nextPortalType = PortalType.Lobby;
      }
      else if (potal.portalType == PortalType.Lobby)
      {
         nextPortalType = PortalType.Map;
      }
      SceneManager.LoadScene(potal.sceneName);
   }
}
