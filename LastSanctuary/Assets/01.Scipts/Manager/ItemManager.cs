using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public int PlayerGold = 10; // 테스트용 추후 삭제

    public bool[] isPlayerHaveRelic = new bool[1]; // 테스트용 추후 삭제
    public void Init() { }
}
