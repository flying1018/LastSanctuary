using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    #region 플레이어의 맵 정보
    public float LastSavePosX;
    public float LastSavePosY;
    public bool IsPlayerClear;
    #endregion


    #region 플레이어의 인벤토리 정보
    public int PlayerGold;
    public bool IsPlayerHaveRelic;
    #endregion

    public SaveData(float _lastSavePosX, float _lastSavePosY, bool _isPlayerClear, int _playerGold, bool _isPlayerHaveRelic)
    {
        LastSavePosX = _lastSavePosX;
        LastSavePosY = _lastSavePosY;
        IsPlayerClear = _isPlayerClear;

        PlayerGold = _playerGold;
        IsPlayerHaveRelic = _isPlayerHaveRelic;
    }

}
