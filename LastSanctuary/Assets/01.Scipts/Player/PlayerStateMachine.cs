using UnityEngine;

/// <summary>
/// 플레이어의 상태를 정의하는 enum
/// </summary>
public enum FSMState
{
    Idle,
    Move,
    Air,
    Jump,
    Attack,
    Guard,
    Fall,
    Ground,
}

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    public Vector2 movementInput;
    public float movementSpeed { get; private set; }
    public Transform MainCameraTransform { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        MainCameraTransform = Camera.main.transform;

        movementSpeed = player.playerData.moveSpeed;

        IdleState = new PlayerIdleState(this);
        MoveState = new PlayerMoveState(this);
        JumpState = new PlayerJumpState(this);
        AttackState = new PlayerAttackState(this);

        ChangeState(IdleState); // 초기 상태
    }


}

