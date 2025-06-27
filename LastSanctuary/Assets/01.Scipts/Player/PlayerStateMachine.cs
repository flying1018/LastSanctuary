using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerGuardState GuardState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerHealState HealState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        MoveState = new PlayerMoveState(this);
        JumpState = new PlayerJumpState(this);
        AttackState = new PlayerAttackState(this);
        DashState = new PlayerDashState(this);
        FallState = new PlayerFallState(this);
        HealState = new PlayerHealState(this);
        ChangeState(IdleState); // 초기 상태
    }


}

