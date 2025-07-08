using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCondition : MonoBehaviour
{
    private int _attack;
    private int _defence;
    private int _maxHp;
    private int _curHp;
    private int _maxGroggyGauge;
    private int _groggyGauge;

    public void Init(Boss boss)
    {
        _attack = boss.Data.attack;
        _defence = boss.Data.defense;
        _maxHp = boss.Data.hp;
        _maxGroggyGauge = boss.Data.groggyGauge;
        
        _curHp = _maxHp;
        _groggyGauge = 0;
    }

}
