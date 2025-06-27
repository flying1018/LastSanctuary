using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //필드
    private PlayerController _input;
    private PlayerStateMachine _stateMachine;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    public SpriteRenderer _spriteRenderer;
    public PlayerCondition _condition;
    //직렬화
    [SerializeField] private PlayerSO playerData;
    //프로퍼티
    public PlayerController Input { get => _input; }
    public PlayerSO Data { get => playerData; }
    public Rigidbody2D Rigidbody { get => _rigidbody; }
    public Animator Animator { get => _animator; }
    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; }
    public PlayerCondition Condition { get => _condition; }

    private void Awake()
    {
        _input = GetComponent<PlayerController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _condition = GetComponent<PlayerCondition>();
        _stateMachine = new PlayerStateMachine(this);
        
    }

    private void Update()
    {
        _stateMachine.HandleInput();
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }
}
