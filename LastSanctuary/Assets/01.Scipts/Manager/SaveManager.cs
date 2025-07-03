using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// 게임 전반부에 세이브할 데이터를 한곳에 모아 Json화
/// </summary>
public class SaveManager : Singleton<SaveManager>
{
    private Vector2 _lastSavePos;

    public void SaveMapItem()
    {
        MapItemData data = new MapItemData(
            1f, 1f, false, 100, false
        );

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        DebugHelper.Log(json);
        JsonSaver.MapItemToJson(json);
    }

    public void SaveMapGimmick()
    {
        
    }

    public void SavePlayerCollect()
    {

    }

    public void SavePlayerStatObject()
    {
        
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
