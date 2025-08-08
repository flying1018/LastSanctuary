using UnityEngine;

public class FastTravelPortal : MonoBehaviour, IInteractable
{
    public string uid;         
    public SanctumArea area;

    [Header("스폰 포인트")]
    public Transform spawnPoint;

    private void Awake()
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
                TeleportTo(target.position);
            }
            else { DebugHelper.LogWarning("최근성역 정보 Null로 뜨니 확인요망"); }
        }
        else
        {  // 성역에서 로비갈때 사용되며 일단 최근성역 등록하ㅔㄱ됨
            
            FastTravelManager.Instance.SetLastSanctum(this);
            var lobby = FastTravelManager.Instance.LobbyPortal;
            if (lobby != null) TeleportTo(lobby.spawnPoint.position);
        }
    }

    private void TeleportTo(Vector3 pos)
    {
        // UIManager.Instance.Fade()

        var player = FindObjectOfType<Player>(); // 너희 프로젝트에 맞춰 참조 교체
        if (player != null) player.transform.position = pos;

        // CameraController.Instance.SnapTo(pos);
    }
}
