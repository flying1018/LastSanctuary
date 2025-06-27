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
    
    //필드
    private CapsuleCollider2D _capsuleCollider;
    private Vector2 _moveInput;
    private bool _isGuarding;
    private bool _isDash;
    private bool _isJump;

    //프로퍼티
    public Vector2 MoveInput => _moveInput; 
    public bool IsGuarding => _isGuarding;
    public bool IsDash  => _isDash; 
    public bool IsJump  => _isJump;
    
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
            _moveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Performed)
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
        if (context.phase == InputActionPhase.Started && IsGround())
        {
            _isJump = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _isJump = false;
        }
    }


    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _isDash = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _isDash = false;
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

    public void OnHeal(InputAction.CallbackContext context)
    {

    }
}
