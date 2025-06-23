using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO playerData { get; private set; }
    [field: SerializeField] public Rigidbody2D rb { get; private set; } 

    [field: Header("Animation")]
    [field: SerializeField] public PlayerAnimationDB playerAnimationDB { get; private set; }

    public Animator animator { get; private set; }
    public PlayerController input { get; private set; }

    public PlayerStateMachine stateMachine;

    private void Awake()
    {
        playerAnimationDB.Initailize();
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        
        
        stateMachine = new PlayerStateMachine(this);
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
