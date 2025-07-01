using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    #region 플레이어의 맵 정보
    public Vector2 LastSavePos;
    public bool[] IsPlayerClear;
    #endregion


    #region 플레이어의 인벤토리 정보
    public int PlayerGold;
    public bool[] IsPlayerHaveRelic;
    #endregion

    public SaveData(Vector2 _lastSavePos, bool[] _isPlayerClear, int _playerGold, bool[] _isPlayerHaveRelic)
    {
        LastSavePos = _lastSavePos;
        IsPlayerClear = _isPlayerClear;

        PlayerGold = _playerGold;
        IsPlayerHaveRelic = _isPlayerHaveRelic;
    }

}
