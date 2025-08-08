using UnityEngine;

public class FastTravelPortal : MonoBehaviour, IInteractable
{
    [Header("ID & 영역")]
    public string uid;                  // 씬 내 유일
    public SanctumArea area;

    [Header("스폰 포인트")]
    public Transform spawnPoint;        // 이 포탈의 도착 위치 (발판 빈 오브젝트)

    private void Awake()
    {
        FastTravelManager.Instance.Register(this);
    }

    public void Interact()
    {
        if (area == SanctumArea.Lobby)
        {
            // 로비 → 최근 성역
            var target = FastTravelManager.Instance.GetLastSanctumSpawn();
            if (target != null) TeleportTo(target.position);
            else Debug.Log("[FastTravel] 최근 성역 정보 없음");
        }
        else
        {
            // 성역 → 로비, 그리고 ‘최근 성역’ 갱신
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
