using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private Vector2 _lastSavePos;

    public void SaveGame()
    {
        SaveData data = new SaveData(
            _lastSavePos,
            GameManager.Instance.IsPlayerClear,
            ItemManager.Instance.PlayerGold,
            ItemManager.Instance.isPlayerHaveRelic
        );

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        JsonSaver.SaveData(json);

    }

    public void SetSavePoint(Vector2 pos)
    {
        _lastSavePos = pos;
        DebugHelper.Log($"새로운 세이브 {_lastSavePos}");
    }

    public Vector2 GetSavePoint()
    {
        return _lastSavePos;
    }
}
