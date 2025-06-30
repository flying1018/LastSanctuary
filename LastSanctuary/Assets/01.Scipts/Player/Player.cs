using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //필드
    public PlayerStateMachine _stateMachine;
    
    //직렬화
    [field: SerializeField] public PlayerAnimationDB AnimationDB { get; private set; }
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private GameObject playerModel;

    //프로퍼티
    public PlayerController Input { get; set; }
    public PlayerSO Data { get => playerData; }
    public Rigidbody2D Rigidbody { get; set; }
    public Animator Animator { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }
    public PlayerCondition Condition { get; set; }
    public GameObject Model { get => playerModel; }


    private void Awake()
    {
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

    /// <summary>
    /// 플레이어가 죽었을때 델리게이트로 호출
    /// 플레이어의 인풋막고, Rigidbody도 멈춰놓음
    /// </summary>
    private void OnDie()
    {
        DebugHelper.Log("OnDie 호출됨");

        Condition.ChangeInvincible(true);
        Animator.SetTrigger("@Die");
        Input.enabled = false;

        Rigidbody.velocity = Vector2.zero;
        // 추후 죽었을때 표기되는 UI, 연출 추가 요망 (화면 암전 등)
        StartCoroutine(Revive_Coroutine());
    }

    /// <summary>
    /// 리스폰할때 실행되는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator Revive_Coroutine()
    {
        DebugHelper.Log("Revive_Coroutine 호출됨");

        yield return new WaitForSeconds(2f); // 앞 애니메이션 대기

        transform.position = SaveManager.Instance.GetSavePoint();
        Animator.SetTrigger("Respawn");
        yield return new WaitForSeconds(2f);

        Condition.PlayerRecovery();
        _stateMachine.ChangeState(_stateMachine.IdleState);
        Animator.Rebind();

        Input.enabled = true;
        yield return new WaitForSeconds(0.5f);
    
        Condition.ChangeInvincible(false);
    }
}
