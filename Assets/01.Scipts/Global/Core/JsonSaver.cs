using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSaver
{
    public static void MapItemToJson(string json)
    {
        string savePath = Path.Combine(
            Application.dataPath + StringNameSpace.GameData.SavePath,
            StringNameSpace.GameData.MapItem
            );
        File.WriteAllText(savePath, json);
    }

    public static void MapGimmickToJson(string json)
    {
        string savePath = Path.Combine(
            Application.dataPath + StringNameSpace.GameData.SavePath,
            StringNameSpace.GameData.Gimmick
            );
        File.WriteAllText(savePath, json);
    }

    public static void PlayerCollectToJson(string json)
    {
        string savePath = Path.Combine(
            Application.dataPath + StringNameSpace.GameData.SavePath,
            StringNameSpace.GameData.Collect
            );
        File.WriteAllText(savePath, json);
    }

    public static void PlayerStatObjectToJson(string json)
    {
        string savePath = Path.Combine(
            Application.dataPath + StringNameSpace.GameData.SavePath,
            StringNameSpace.GameData.PlayerStat
            );
        File.WriteAllText(savePath, json);
    }
}
