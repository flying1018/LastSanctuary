using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어의 조작을 처리하는 메서드
/// </summary>
public class PlayerController : MonoBehaviour
{
    //필드
    private float _dashCoolTime;
    private bool _dashCool;

    //프로퍼티
    public Vector2 MoveInput { get; set; }
    public IInteractable InteractableTarget { get; set; }
    public bool IsGuarding { get; set; }
    public bool IsDash { get; set; }
    public bool IsJump { get; set; }
    public bool IsHoldJump { get; set; }
    public bool IsHeal { get; set; }
    public bool IsAttack { get; set; }
    public bool IsNearInteractable { get; set; }
    public bool IsSavePoint { get; set; }


    public void Start()
    {
        _dashCoolTime = GetComponent<Player>().Data.dashCoolTime;
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
        if (context.phase == InputActionPhase.Started)
        {
            IsJump = true;
        }
        if (context.phase == InputActionPhase.Performed)
        {
            IsHoldJump = true;
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            IsJump = false;
            IsHoldJump = false;
        }

    }


    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && !_dashCool)
        {
            IsDash = true;
            _dashCool = true;
            Invoke(nameof(DashCoolTime), _dashCoolTime);
        }
        else if (context.phase == InputActionPhase.Canceled)
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (!IsNearInteractable || InteractableTarget == null) { return; } // 방어코드

            InteractableTarget.Interact();
        }
    }

    void DashCoolTime()
    {
        _dashCool = false;
    }

}
