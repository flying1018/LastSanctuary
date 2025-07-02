using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationDB
{
    [SerializeField] private string groundParameter = "@Ground";
    [SerializeField] private string idleParameter = "Idle";
    [SerializeField] private string moveParameter = "Move";
    [SerializeField] private string HealParameter = "Heal"; //힐 애니메이션
    
    [SerializeField] private string airParameter = "@Air";
    [SerializeField] private string fallParameter = "Fall";
    [SerializeField] private string jumpParameter = "Jump";

    [SerializeField] private string attackParameter = "@Attack";
    [SerializeField] private string comboParameter = "Combo";
    [SerializeField] private string guardParameter = "Guard";
    
    [SerializeField] private string dieParameter = "@Die";


    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int MoveParameterHash { get; private set; }
    public int HealParameterHash { get; private set; } //힐 해시값
    
    public int AirParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int ComboParameterHash { get; private set; }   
    public int GuardParameterHash { get; private set; }

    public int DieParameterHash { get; private set; }

    public void Initailize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameter);
        IdleParameterHash = Animator.StringToHash(idleParameter);
        MoveParameterHash = Animator.StringToHash(moveParameter);
        HealParameterHash = Animator.StringToHash(HealParameter); //힐 정수값으로
        
        AirParameterHash = Animator.StringToHash(airParameter);
        FallParameterHash = Animator.StringToHash(fallParameter);
        JumpParameterHash = Animator.StringToHash(jumpParameter);

        AttackParameterHash = Animator.StringToHash(attackParameter);
        ComboParameterHash = Animator.StringToHash(comboParameter);  
        GuardParameterHash = Animator.StringToHash(guardParameter);

        DieParameterHash = Animator.StringToHash(dieParameter);
    }
}
