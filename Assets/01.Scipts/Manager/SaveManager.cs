using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// 게임 전반부에 세이브할 데이터를 한곳에 모아 Json화
/// </summary>
public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private Transform _lastSavePos;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    
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

    //세이브 포인트 지정
    public void SetSavePoint(Vector2 pos)
    {
        _lastSavePos.position = pos;
        DebugHelper.Log($"새로운 세이브 {_lastSavePos.position}");
    }

    //세이브 포인틀 리턴
    public Vector2 GetSavePoint()
    {
        return _lastSavePos.position;
    }
}
