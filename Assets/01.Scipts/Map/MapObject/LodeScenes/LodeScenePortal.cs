using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PortalType
{
    Lobby,
    Map,
}

public class LodeScenePortal : MonoBehaviour, IInteractable
{
    public string uid;
    public string sceneName;
    public Transform spawnPoint;
    public PortalType portalType;
    
    public void Interact()
    {
        LodeScenesManager.Instance.LoadScenePortal(this);
    }

    public IEnumerator<LodeScenePortal> GetEnumerator()
    {
        throw new System.NotImplementedException();
    }
}
