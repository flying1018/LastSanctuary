using UnityEngine;

public class EnemyData
{
    public string Id { get; }
    public string Name { get; }
    public bool IsGround { get; }
    public float Attack { get; }
    public float Defense { get; }
    public int Hp { get; }
    public int AttackRange { get; }
    public float MoveSpeed { get; }
    public float AreaRange { get; }

    public EnemyData(
        string _id, bool _isGround, string _name, float _attack, float _defense,
        int _hp, int _attackRange, float _moveSpeed, float _areaRange
        )
    {
        Id = _id;
        IsGround = _isGround;
        Name = _name;
        Attack = _attack;
        Defense = _defense;
        Hp = _hp;
        AttackRange = _attackRange;
        MoveSpeed = _moveSpeed;
        AreaRange = _areaRange;
    }

    public EnemyData() { }
}