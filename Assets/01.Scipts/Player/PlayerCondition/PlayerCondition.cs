using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public partial class PlayerCondition : Condition
{
    private Player _player;
    private float _curStamina;
    private float _maxStamina;
    private int _staminaRecovery;
    private float _curUltimate;
    private float _maxUltimate;

    private Dictionary<StatObjectSO, Coroutine> _tempBuffs = new();

    //체력 스태미나 관련 프로퍼티
    public float MaxHp
    {
        get => _maxHp; 
        set => _maxHp = Mathf.Max(0, value);
    }
    public float CurHp
    {
        get => _curHp; 
        set => _curHp = Mathf.Clamp(value, 0, TotalHp);
    }
    public float TotalHp
    {
        get => _maxHp + _player.Inventory.EquipHp + BuffHp;
    }
    public float MaxStamina
    {
        get => _maxStamina;
        set => _maxStamina = Mathf.Max(0, value);
    }
    public float CurStamina
    {
        get => _curStamina;
        set => _curStamina = Mathf.Clamp(value, 0, TotalStamina);
    }
    public float TotalStamina
    {
        get => MaxStamina + BuffStamina + _player.Inventory.EquipStamina;
    }

    //공격력 방어력 프로퍼티
    public int Attack
    {
        get => _attack; 
        set => _attack = Mathf.Max(0, value);
    }

    public int TotalAttack
    {
        get => _attack + BuffAtk + _player.Inventory.EquipAtk;
    }
    public int Defence
    {
        get => _defence; 
        set => _defence =  Mathf.Max(0, value);
    }
    public int TotalDefence
    {
        get => _defence + BuffDef + _player.Inventory.EquipDef;
    }
    
    //성물로 증가 가능한 프로퍼티
    public int HealAmonut { get; set; } //이건 다른 스크립트로 옮겨도 될듯.
    public float MaxUltimate
    {
        get => _maxUltimate; 
        set => _maxUltimate = Mathf.Max(0, value);
    }
    public float CurUltimate 
    { 
        get => _curUltimate; 
        set => _curUltimate = Mathf.Clamp(value , 0, MaxUltimate);
    }

    //버프 표시 프로퍼티
    public int BuffHp { get; set; }
    public float BuffStamina { get; set; }
    public int BuffAtk { get; set; }
    public int BuffDef { get; set; }

    //가드 프로퍼티
    public DamageType DamageType { get; set; }
    public bool IsGuard { get; set; }
    public bool IsPerfectGuard { get; set; }

    //무적 처리 프로퍼티
    public bool IsInvincible { get; set; }
    public bool DontKnockBack { get; set; }
    

    #region Condition

    //플레이어 회복
    public void PlayerRecovery()
    {
        CurHp = TotalHp;
        CurStamina = TotalStamina;
    }

    //플레이어 체력 회복
    public void Heal() { CurHp += HealAmonut; }

    //스테미나 사용
    public bool UsingStamina(int stamina)
    {
        if (_curStamina >= stamina)
        {
            CurStamina -= stamina;
            return true;
        }
        return false;
    }

    //스테미나 회복
    public void RecoveryStamina() { CurStamina += _staminaRecovery * Time.deltaTime; }
    
    #endregion

    #region Buff

    //버프 적용
    public void ApplyTempBuff(StatObjectSO data)
    {
        if (_tempBuffs.TryGetValue(data, out Coroutine lastCoroutine))
        {
            StopCoroutine(lastCoroutine);
            RemoveTempBuff(data);
            _tempBuffs.Remove(data);
        }

        foreach (StatDelta stat in data.statDeltas)
        {
            switch (stat.statType)
            {
                case StatType.Hp:
                    BuffHp += stat.amount;
                    CurHp += stat.amount;
                    break;
                case StatType.Stamina:
                    BuffStamina += stat.amount;
                    break;
                case StatType.Atk :
                    BuffAtk += stat.amount;
                    break;
                case StatType.Def :
                    BuffDef += stat.amount;
                    break;
            }
        }

        Coroutine newCoroutine = StartCoroutine(BuffDurationTimerCoroutine(data));
        _tempBuffs[data] = newCoroutine;
    }

    public void ApplyPermanent(StatObjectSO data)
    {
        foreach (StatDelta stat in data.statDeltas)
        {
            switch (stat.statType)
            {
                case StatType.Hp:
                    MaxHp += stat.amount;
                    CurHp += stat.amount;
                    break;
                case StatType.Stamina:
                    MaxStamina += stat.amount;
                    break;
                case StatType.Atk :
                    Attack += stat.amount;
                    break;
                case StatType.Def :
                    Defence += stat.amount;
                    break;
                case StatType.Ultimit:
                    CurUltimate = MaxUltimate;
                    break;
            }
        }
    }

    //버프 제거
    private void RemoveTempBuff(StatObjectSO data)
    {
        foreach (StatDelta stat in data.statDeltas)
        {
            switch (stat.statType)
            {
                case StatType.Hp:
                    BuffHp -= stat.amount;
                    CurHp -= stat.amount;
                    break;
                case StatType.Stamina:
                    BuffStamina -= stat.amount;
                    break;
                case StatType.Atk :
                    BuffAtk -= stat.amount;
                    break;
                case StatType.Def :
                    BuffDef -= stat.amount;
                    break;
            }
        }
        //죽었을떄는 전체 초기화
    }

    //버프 지속시간 타이머
    private IEnumerator BuffDurationTimerCoroutine(StatObjectSO data)
    {
        yield return new WaitForSeconds(data.duration);
        RemoveTempBuff(data);
        _tempBuffs.Remove(data);
    }
    #endregion

    #region UI

    //UI에 사용되는 hp
    public float HpValue()
    {
        return _curHp / TotalHp;
    }

    //UI가 사용되는 스태미나
    public float StaminaValue()
    {
        return _curStamina / TotalStamina;;
    }

    public float UltimateValue()
    {
        return _curUltimate / MaxUltimate;
    }

    #endregion
}
