using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public int PlayerGold { get; set; }

    public bool[] isPlayerHaveRelic { get; set; }
    public void Init() { }
}
