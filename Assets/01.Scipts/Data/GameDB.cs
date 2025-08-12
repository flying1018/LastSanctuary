using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameDB
{
    public static Dictionary<string, EnemyDB> enemyData;
    // public static Dictionary<string, ItemData> itemData;
    // public static Dictionary<string, PlayerData> playerData;

    public static async Task Init()
    {
        enemyData = await JsonLoader.ParseEnemyJson(StringNameSpace.JsonAddress.EnemyJsonAddress);
        DebugHelper.Log($"{enemyData["Goblin01"].Defense}");
    }
}