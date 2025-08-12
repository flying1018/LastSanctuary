using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SanctumArea
{
    Lobby,
    Map1,
    Map2,
    Map3,
    Map4
}

public class FastTravelManager : Singleton<FastTravelManager>
{
    private readonly Dictionary<string, FastTravelPortal> _byUid = new();
    private readonly Dictionary<SanctumArea, FastTravelPortal> _byArea = new();

    public string LastSanctumPortalUid { get; private set; } 
    public FastTravelPortal LobbyPortal { get; private set; }

    private string _playerPrefsKey = StringNameSpace.FastTravel.PlayerPrefsKey;
    private Vector3? _teleportPosition;

    protected override void Awake()
    {
        
        LastSanctumPortalUid = PlayerPrefs.GetString(_playerPrefsKey, string.Empty);
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this);
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Register(FastTravelPortal portal)
    {
        if (!string.IsNullOrEmpty(portal.uid)) _byUid[portal.uid] = portal;
        _byArea[portal.area] = portal;
        if (portal.area == SanctumArea.Lobby) LobbyPortal = portal;
    }

    public void SetLastSanctum(FastTravelPortal portal)
    {
        if (portal.area == SanctumArea.Lobby) return;
        LastSanctumPortalUid = portal.uid;
        PlayerPrefs.SetString(_playerPrefsKey, LastSanctumPortalUid); 
    }

    public Transform GetLastSanctumSpawn()
    {
        if (string.IsNullOrEmpty(LastSanctumPortalUid)) return null;
        return _byUid.TryGetValue(LastSanctumPortalUid, out var p) ? p.spawnPoint : null;
    }

    public void TravelTo(FastTravelPortal portal)
    {
        if (portal == null) return;
        _teleportPosition = portal.spawnPoint.position;
        SceneManager.LoadSceneAsync(portal.sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var portals = FindObjectsOfType<FastTravelPortal>();
        foreach (var portal in portals)
        {
            Register(portal);
        }

        if (_teleportPosition.HasValue)
        {
            var player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.transform.position = _teleportPosition.Value;
            }
            _teleportPosition = null;
        }
    }
}
