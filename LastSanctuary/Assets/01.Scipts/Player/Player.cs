using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    //필드
    private PlayerStateMachine _stateMachine;
    private CapsuleCollider2D _capsuleCollider;
    
    //직렬화
    [field: SerializeField] public PlayerAnimationDB AnimationDB { get; private set; }
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private float downJumpTime;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask aerialPlatformLayer;
    
    //프로퍼티
    public PlayerController Input { get; set; }
    public Rigidbody2D Rigidbody { get; set; }
    public Animator Animator { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }
    public PlayerCondition Condition { get; set; }
    public PlayerSO Data { get => playerData; }
    public GameObject Model { get=> playerModel; }
    public bool IsLadder { get; set; }
    public AerialPlatform AerialPlatform { get; set; }
    

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        Input = GetComponent<PlayerController>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Condition = GetComponent<PlayerCondition>();
        SpriteRenderer = playerModel.GetComponent<SpriteRenderer>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(StringNameSpace.Tags.Ladder))
        {
            IsLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(StringNameSpace.Tags.Ladder))
        {
            IsLadder = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.AerialPlatform))
        {
            AerialPlatform = other.gameObject.GetComponent<AerialPlatform>();
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.AerialPlatform))
        {
            AerialPlatform = null;
        }
    }


    #region Need MonoBehaviour Method
    
    //바닥 확인
    public bool IsGround()
    {
        Debug.DrawRay(transform.position, Vector2.down * (_capsuleCollider.size.y / 2 +groundCheckDistance), Color.red);
        return Physics2D.Raycast(transform.position, Vector2.down,
            (_capsuleCollider.size.y/2)+groundCheckDistance, groundLayer);
    }

    public bool IsAerialPlatform()
    {
        return Physics2D.Raycast(transform.position, Vector2.down,
            (_capsuleCollider.size.y/2)+groundCheckDistance,aerialPlatformLayer);
    }

    /// <summary>
    /// 플레이어가 죽었을때 델리게이트로 호출
    /// 플레이어의 인풋막고, Rigidbody도 멈춰놓음
    /// </summary>
    private void OnDie()
    {
        Animator.SetTrigger("@Die");
        Input.enabled = false;
        Rigidbody.velocity = Vector2.zero;
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

        Animator.SetTrigger("@Respawn");
        Input.enabled = true;
    }
    
    #endregion
}
