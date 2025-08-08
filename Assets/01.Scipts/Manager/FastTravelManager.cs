using System.Collections.Generic;
using UnityEngine;

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

    protected override void Awake()
    {
        LastSanctumPortalUid = PlayerPrefs.GetString(_playerPrefsKey, string.Empty);
    }

    public void Register(FastTravelPortal p)
    {
        if (!string.IsNullOrEmpty(p.uid)) _byUid[p.uid] = p;
        _byArea[p.area] = p;
        if (p.area == SanctumArea.Lobby) LobbyPortal = p;
    }

    public void SetLastSanctum(FastTravelPortal p)
    {
        if (p.area == SanctumArea.Lobby) return;
        LastSanctumPortalUid = p.uid;
        PlayerPrefs.SetString(_playerPrefsKey, LastSanctumPortalUid); 
    }

    public Transform GetLastSanctumSpawn()
    {
        if (string.IsNullOrEmpty(LastSanctumPortalUid)) return null;
        return _byUid.TryGetValue(LastSanctumPortalUid, out var p) ? p.spawnPoint : null;
    }
}
