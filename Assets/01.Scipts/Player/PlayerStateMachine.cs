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
    public List<PlayerAttackState> ComboAttack { get; private set; }
    public StrongAttackState StrongAttack;
    public JumpAttackState JumpAttack { get; private set; }
    public DashAttackState DashAttack { get; private set; }
    public UltimateState UltState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerGuardState GuardState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerHealState HealState { get; private set; }
    public PlayerHitState HitState { get; private set; }
    public PlayerRopeIdleState RopeIdleState { get; private set; }
    public PlayerRopeMoveState RopeMoveState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }
    public PlayerRespawnState RespawnState { get; private set; }
    public PlayerInteractState InteractState { get; private set; }
    public PlayerTopAttackState TopAttack { get; private set; }
    public PlayerJumpTopAttackState JumpTopAttack { get; private set; }
    public GroggyAttackState GroggyAttackState { get; private set; }
    
    public int comboIndex;


    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        MoveState = new PlayerMoveState(this);
        JumpState = new PlayerJumpState(this);
        DownJumpState = new PlayerDownJumpState(this);
        StrongAttack = new StrongAttackState(this, player.AttackData.strongAttack);
        ComboAttack = new List<PlayerAttackState>
        {
            new ComboAttackState(this, player.AttackData.attacks[0]),
            new ComboAttackState(this, player.AttackData.attacks[1]),
        };
        ComboAttack.Add(StrongAttack);
        JumpAttack = new JumpAttackState(this, player.AttackData.jumpAttack);
        DashAttack = new DashAttackState(this, player.AttackData.dashAttack);
        UltState = new UltimateState(this, player.AttackData.ultAttack);
        DashState = new PlayerDashState(this);
        FallState = new PlayerFallState(this);
        HealState = new PlayerHealState(this);
        GuardState = new PlayerGuardState(this);
        RopeIdleState = new PlayerRopeIdleState(this);
        RopeMoveState = new PlayerRopeMoveState(this);
        HitState = new PlayerHitState(this);
        DeathState = new PlayerDeathState(this);
        RespawnState = new PlayerRespawnState(this);
        InteractState = new PlayerInteractState(this);
        TopAttack = new PlayerTopAttackState(this, player.AttackData.topAttack);
        JumpTopAttack = new PlayerJumpTopAttackState(this, player.AttackData.jumpTopAttack);
        GroggyAttackState = new GroggyAttackState(this, player.AttackData.groggyAttack);
        
        comboIndex = 0;
        ChangeState(IdleState); // 초기 상태
    }


}

