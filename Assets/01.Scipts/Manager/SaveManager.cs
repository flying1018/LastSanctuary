using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// 게임 전반부에 세이브할 데이터를 한곳에 모아 Json화
/// </summary>
public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private Transform lastSavePos;

    //세이브 포인트 지정
    public void SetSavePoint(Vector2 pos)
    {
        lastSavePos.position = pos;
    }

    //세이브 포인틀 리턴
    public Vector2 GetSavePoint()
    {
        return lastSavePos.position;
    }

    public void SaveGame(int index)
    {
        var mapItem = SaveMapItemData();
        var playerStat = SavePlayerStatData();
        var gimmick = GatherGimmickData();
        var collect = GatherCollectData();

        if (index == 1)
        {
            string playerStatJson = JsonConvert.SerializeObject(playerStat, Formatting.Indented);
            string gimmickJson = JsonConvert.SerializeObject(gimmick, Formatting.Indented);
            string collectJson = JsonConvert.SerializeObject(collect, Formatting.Indented);

            JsonSaver.PlayerStatObjectToJson(playerStatJson);
            JsonSaver.MapGimmickToJson(gimmickJson);
            JsonSaver.PlayerCollectToJson(collectJson);
        }
        else if (index == 2)
        {

            string playerStatJson = JsonConvert.SerializeObject(playerStat, Formatting.Indented);
            string mapItemJson = JsonConvert.SerializeObject(mapItem, Formatting.Indented);

            DebugHelper.Log(mapItemJson);
            JsonSaver.MapItemToJson(mapItemJson);
            JsonSaver.PlayerStatObjectToJson(playerStatJson);
        }
        else { return; }
    }

    private MapItemData SaveMapItemData()
    {
        Vector2 pos = lastSavePos.position;
        // bool clear = 
        // int gold = 
        // bool haveRelic = 
        return new MapItemData(pos.x, pos.y, true, 100, true);
    }

    private PlayerStatData SavePlayerStatData()
    {
        return null;
    }

    private GimmickData GatherGimmickData()
    {
        return null;
    }

    private CollectData GatherCollectData()
    {
        return null;
    }
}
