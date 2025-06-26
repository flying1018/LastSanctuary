using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //직렬화
    [SerializeField] private LayerMask groundLayer;
    //필드
    private Vector2 _moveInput;
    private bool _isGuarding;
    
    //프로퍼티
    public Vector2 MoveInput { get => _moveInput; }
    public bool IsGuarding => _isGuarding; 
    
    public bool IsGround()
    {
        //이거 하드코딩임.
        var hit = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, groundLayer);
        return hit;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _moveInput = context.ReadValue<Vector2>();
        }  
        else if (context.phase == InputActionPhase.Canceled)
        {
            _moveInput = Vector2.zero;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Started)
        {

        }
    }
    public void OnGuard(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _isGuarding = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _isGuarding = false;
        }
    }
}
