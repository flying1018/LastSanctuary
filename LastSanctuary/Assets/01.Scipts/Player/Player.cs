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
    public PlayerCondition _condition;
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
    public PlayerCondition Condition { get => _condition; }
    public GameObject Model { get => playerModel; }

    private void Awake()
    {
        _input = GetComponent<PlayerController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _condition = GetComponent<PlayerCondition>();
        _spriteRenderer = playerModel.GetComponent<SpriteRenderer>();
        AnimationDB.Initailize();
        _stateMachine = new PlayerStateMachine(this);

        GetComponent<PlayerCondition>().OnDie += OnDie;
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

    /// <summary>
    /// 플레이어가 죽었을때 델리게이트로 호출
    /// 플레이어의 인풋막고, Rigidbody도 멈춰놓음
    /// </summary>
    private void OnDie()
    {
        _animator.SetTrigger("@Die");
        _input.enabled = false;
        _rigidbody.velocity = Vector2.zero;
        // 추후 죽었을때 표기되는 UI추가 요망
    }

    /// <summary>
    /// 리스폰할때 실행되는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator Respawn_Coroutine()
    {
        yield return new WaitForSeconds(2f);
        transform.position = SaveManager.Instance.GetSavePoint();

        _animator.SetTrigger("@Respawn");
        _input.enabled = true;
    }
}
