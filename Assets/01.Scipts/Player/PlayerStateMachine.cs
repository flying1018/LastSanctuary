using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 상태 머신
/// </summary>
public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerDownJumpState DownJumpState { get; private set; }
    public List<ComboAttackState> ComboAttack { get; private set; }
    public JumpAttackState JumpAttack { get; private set; }
    public DashAttackState DashAttack { get; private set; }
    public UltimateState UltState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerGuardState GuardState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerHealState HealState { get; private set; }
    public PlayerHitState HitState { get; private set; }
    public PlayerRopedState RopedState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }
    public PlayerRespawnState RespawnState { get; private set; }
    public PlayerInteractState InteractState { get; private set; }
    public PlayerTopAttackState TopAttackState { get; private set; }
    public GroggyAttackState GroggyAttackState { get; private set; }
    
    public int comboIndex;


    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        MoveState = new PlayerMoveState(this);
        JumpState = new PlayerJumpState(this);
        DownJumpState = new PlayerDownJumpState(this);
        ComboAttack = new List<ComboAttackState>
        {
            new ComboAttackState(this, player.Data.attacks[0]),
            new ComboAttackState(this, player.Data.attacks[1]),
            new ComboAttackState(this, player.Data.attacks[2])
        };
        JumpAttack = new JumpAttackState(this, player.Data.jumpAttack);
        DashAttack = new DashAttackState(this, player.Data.dashAttack);
        UltState = new UltimateState(this, player.Data.UltAttack);
        DashState = new PlayerDashState(this);
        FallState = new PlayerFallState(this);
        HealState = new PlayerHealState(this);
        GuardState = new PlayerGuardState(this);
        RopedState = new PlayerRopedState(this);
        HitState = new PlayerHitState(this);
        DeathState = new PlayerDeathState(this);
        RespawnState = new PlayerRespawnState(this);
        InteractState = new PlayerInteractState(this);
        TopAttackState = new PlayerTopAttackState(this, player.Data.topAttack);
        GroggyAttackState = new GroggyAttackState(this, player.Data.groggyAttack);
        
        comboIndex = 0;
        ChangeState(IdleState); // 초기 상태
    }


}

