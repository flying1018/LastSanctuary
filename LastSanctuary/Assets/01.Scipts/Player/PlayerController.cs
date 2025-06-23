using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player Player;
    public InputSystem playerInputs { get; private set; }
    public InputSystem.PlayerActions playerActions { get; private set; }

    public Vector2 MoveInput { get; private set; }
    private bool isGround;

    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        playerInputs = new InputSystem();
        playerActions = playerInputs.Player;
        Player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        IsGround();
    }

    private void Move(float dic)
    {
        Player.rb.velocity = new Vector2(dic * Player.playerData.moveSpeed, Player.rb.velocity.y);
        Player.stateMachine.ChangeState(Player.stateMachine.MoveState);
    }

    private void Stop()
    {
        Player.rb.velocity = new Vector2(0, Player.rb.velocity.y);

        Player.stateMachine.ChangeState(Player.stateMachine.IdleState);
    }

    private void IsGround()
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, groundLayer);
        DebugHelper.ShowRay(this.transform.position, Vector2.down * 1.2f, Color.red);
        //DebugHelper.Log(isGround.ToString());
    }

    #region InputSystem
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (context.phase == InputActionPhase.Started)
        {
            if (moveInput.x < 0)
            {
                Move(-1);
            }
            else if (moveInput.x > 0)
            {
                Move(1);
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            Stop();
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Started)
        {
            if (!isGround) { DebugHelper.Log("점프 리턴됨");
                return; }

            Player.stateMachine.ChangeState(Player.stateMachine.JumpState);
        }
    }

    #endregion
}
