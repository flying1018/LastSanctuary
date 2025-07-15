using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 몬스터에 필요한 애니메이션 해쉬값 
/// </summary>
[Serializable]
public class EnemyAnimationDB
{
    [SerializeField] private string idleParameter = "Idle";
    [SerializeField] private string walkParameter = "Walk";
    [SerializeField] private string attackParameter = "Attack";
    [SerializeField] private string hitParameter = "Hit";
    [SerializeField] private string deathParameter = "Death";



    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }
    public int DeathParameterHash { get; private set; }


    public void Initailize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameter);
        WalkParameterHash = Animator.StringToHash(walkParameter);
        AttackParameterHash = Animator.StringToHash(attackParameter);
        HitParameterHash = Animator.StringToHash(hitParameter);
        DeathParameterHash = Animator.StringToHash(deathParameter);
    }
}
