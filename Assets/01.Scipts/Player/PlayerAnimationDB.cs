using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 플레이어 애니메이션 해쉬 데이터
/// </summary>
[Serializable]
public class PlayerAnimationDB
{
    [SerializeField] private string groundParameter = "@Ground";
    [SerializeField] private string idleParameter = "Idle";
    [SerializeField] private string moveParameter = "Move";
    [SerializeField] private string healParameter = "Heal"; 
    [SerializeField] private string interactionParameter = "Interaction"; 
    
    [SerializeField] private string airParameter = "@Air";
    [SerializeField] private string fallParameter = "Fall";
    [SerializeField] private string jumpParameter = "Jump";
    [SerializeField] private string ropedParameter = "Roped";

    [SerializeField] private string attackParameter = "@Attack";
    [SerializeField] private string comboParameter = "Combo";
    [SerializeField] private string guardParameter = "Guard";
    
    [SerializeField] private string dieParameter = "@Die";
    [SerializeField] private string respawnParameter = "Respawn";
    [SerializeField] private string dashParameter = "Dash";


    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int MoveParameterHash { get; private set; }
    public int HealParameterHash { get; private set; } 
    public int InteractionParameter { get; private set; } 
    
    public int AirParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int RopedParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int ComboParameterHash { get; private set; }   
    public int GuardParameterHash { get; private set; }

    public int DieParameterHash { get; private set; }
    public int RespawnParameterHash { get; private set; }
    public int DashParameterHash { get; private set; }

    public void Initailize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameter);
        IdleParameterHash = Animator.StringToHash(idleParameter);
        MoveParameterHash = Animator.StringToHash(moveParameter);
        HealParameterHash = Animator.StringToHash(healParameter); 
        InteractionParameter = Animator.StringToHash(interactionParameter);
        
        AirParameterHash = Animator.StringToHash(airParameter);
        FallParameterHash = Animator.StringToHash(fallParameter);
        JumpParameterHash = Animator.StringToHash(jumpParameter);
        RopedParameterHash = Animator.StringToHash(ropedParameter);       

        AttackParameterHash = Animator.StringToHash(attackParameter);
        ComboParameterHash = Animator.StringToHash(comboParameter);  
        GuardParameterHash = Animator.StringToHash(guardParameter);

        DieParameterHash = Animator.StringToHash(dieParameter);
        RespawnParameterHash = Animator.StringToHash(respawnParameter);
        DashParameterHash = Animator.StringToHash(dashParameter);
    }
}
