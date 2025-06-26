using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Json 형식의 데이터를 실제 사용할 데이터 클래스로 변환하는 유틸 클래스
/// </summary>
public class JsonLoader
{
    /// <summary>
    /// Json형식의 적 데이터를 파싱하여 Dictionary<string, EnemyData>형식으로 반환
    /// </summary>
    /// <param name="address">적데이터 Json파일 어드레서블 주소를 넣으면 됨</param>
    /// <returns> Key = 적 ID, Value = EnemyData, 반환 확인은 DataDB.cs 확인 요망</returns>
    public static async Task<Dictionary<string, EnemyData>> ParseEnemyJson(string address)
    {
        JToken root = await LoadJson(address);
        if (root == null) { DebugHelper.LogWarning("오류로 파싱 실패"); return null; }

        JToken enemies = root["Enemy"];
        Dictionary<string, EnemyData> enemyData = new Dictionary<string, EnemyData>();

        for (int i = 0; i < enemies.Count(); i++)
        {
            string _id = (string)enemies[i]["Id"];
            bool _isGround = (bool)enemies[i]["IsGround"];
            string _name = (string)enemies[i]["Name"];
            float _attack = (float)enemies[i]["Attack"];
            float _defense = (float)enemies[i]["Defense"];
            int _hp = (int)enemies[i]["Hp"];
            int _attackRange = (int)enemies[i]["AttackRange"];
            float _moveSpeed = (float)enemies[i]["MoveSpeed"];
            float _areaRange = (float)enemies[i]["AreaRange"];

            EnemyData Data = new EnemyData(_id, _isGround, _name, _attack, _defense, _hp, _attackRange, _moveSpeed, _areaRange);

            if (!enemyData.ContainsKey(_id)) { enemyData[_id] = Data; }
            else { DebugHelper.LogError($"{_id}가 겹치는 중복된키 있음 확인 요망"); }

        }

        return enemyData;
    }

    // 추후 확장용 메서드, 추가 예정
    // public static async Task<Dictionary<string, EnemyData>> ParseItemJson(string address)
    // {
    //     JToken root = await LoadJson(address);
    //     if (root == null) { DebugHelper.LogWarning("오류로 파싱 실패"); return null; }
    // }

    /// <summary>
    /// Json파일을 어드레서블로 불러와 Text로 바꿔주는 메서드
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public static async Task<JToken> LoadJson(string address)
    {
        var handle = Addressables.LoadAssetAsync<TextAsset>(address);
        var result = await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            DebugHelper.Log("로딩된 Json " + result.text);
        }
        else
        {
            DebugHelper.LogError("Json로드 실패, 주소 재확인 요망");
            return null;
        }

        return JToken.Parse(result.text);
    }
}