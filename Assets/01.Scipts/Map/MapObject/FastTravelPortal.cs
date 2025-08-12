using UnityEngine;

public class FastTravelPortal : MonoBehaviour, IInteractable
{
    public string uid;         
    public SanctumArea area;
    public string sceneName;
    public Transform spawnPoint;

    private void Start()
    {
        FastTravelManager.Instance.Register(this);
    }

    public void Interact()
    {
        
        if (area == SanctumArea.Lobby)
        {
            var target = FastTravelManager.Instance.GetLastSanctumSpawn();
            if (target != null)
            {
               FastTravelManager.Instance.TravelTo(target.GetComponent<FastTravelPortal>());
            }
            else { DebugHelper.LogWarning("최근성역 정보 Null로 뜨니 확인요망"); }
        }
        else
        {  // 성역에서 로비로 이동 (마지막 성역 갱신)
            
            FastTravelManager.Instance.SetLastSanctum(this);
            var lobby = FastTravelManager.Instance.LobbyPortal;
            Debug.Log(lobby);
            if (lobby != null) FastTravelManager.Instance.TravelTo(lobby);
        }
    }
    
}
