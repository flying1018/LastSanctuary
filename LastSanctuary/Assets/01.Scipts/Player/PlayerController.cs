using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //직렬화
    [SerializeField] private float dashCoolTime;
    
    //필드
    private bool _dashCool;
    
    //프로퍼티
    public Vector2 MoveInput { get; set; }
    public bool IsGuarding { get; set; }
    public bool IsDash { get; set; }
    public bool IsJump { get; set; }
    public bool IsHeal { get; set; }
    public bool IsAttack { get; set; }

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
        if (context.phase == InputActionPhase.Started)
        {
            IsJump = true;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            IsJump = false;       
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
        else
        {
            IsDash = false;
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
}
