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
    private SpriteRenderer _spriteRenderer;

    //직렬화
    [field: SerializeField] public PlayerAnimationDB AnimationDB { get; private set; }
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private GameObject playerModel;
    
    //프로퍼티
    public PlayerController Input { get => _input; }
    public PlayerSO Data { get => playerData; }
    public Rigidbody2D Rigidbody { get => _rigidbody; }
    public Animator Animator { get => _animator; }
    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; }
    public GameObject Model { get => playerModel; }

    private void Awake()
    {
        _input = GetComponent<PlayerController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = playerModel.GetComponent<SpriteRenderer>();
        AnimationDB.Initailize();
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
