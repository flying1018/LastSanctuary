using UnityEngine;

public class EnemySO : ScriptableObject
{
    [Header("GroundState")] 
    public float moveSpeed;
    public float areaRange;
    
    [Header("BattleState")]
    public int damage;
    public int hp;
    public int defense;
    public int attackRange;
}
