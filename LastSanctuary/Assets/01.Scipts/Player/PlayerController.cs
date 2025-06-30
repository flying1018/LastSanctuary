using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //직렬화
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float dashCoolTime;
    
    //필드
    private CapsuleCollider2D _capsuleCollider;
    private bool _dashCool;
    
    //프로퍼티
    public Vector2 MoveInput { get; set; }
    public bool IsGuarding { get; set; }
    public bool IsDash { get; set; }
    public bool IsJump { get; set; }
    public bool IsHeal { get; set; }
    public bool IsAttack { get; set; }
    
    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public bool IsGround()
    {
        Debug.DrawRay(transform.position, Vector2.down * (_capsuleCollider.size.y / 2 +groundCheckDistance), Color.red);
        return Physics2D.Raycast(transform.position, Vector2.down,
            (_capsuleCollider.size.y/2)+groundCheckDistance, groundLayer);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            MoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Performed)
        {
            MoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            MoveInput = Vector2.zero;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGround())
        {
            IsJump = true;
        }
    }


    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && !_dashCool)
        {
            IsDash = true;
            _dashCool = true;
            Invoke(nameof(DashCoolTime), dashCoolTime);
        }
    }


    public void OnGuard(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Started)
        {
            IsGuarding = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            IsGuarding = false;
        }
    }

    public void OnHeal(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            IsHeal = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            IsHeal = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            IsAttack = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            IsAttack = false;
        }
    }
    
    void DashCoolTime()
    {
        _dashCool = false;
    }
    
    public void testGuard(bool state)
    {
        IsGuarding = state;
    }

}
