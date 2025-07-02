using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //필드
    private CapsuleCollider2D _capsuleCollider;
    
    //직렬화
    [field: SerializeField] public PlayerAnimationDB AnimationDB { get; private set; }
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask aerialPlatformLayer;
    [SerializeField] private GameObject weapon;
    
    //프로퍼티
    public PlayerStateMachine StateMachine { get; set; }
    public PlayerController Input { get; set; }
    public Rigidbody2D Rigidbody { get; set; }
    public Animator Animator { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }
    public PlayerCondition Condition { get; set; }
    public bool IsLadder { get; set; }
    public AerialPlatform AerialPlatform { get; set; }
    //직렬화 데이터 프로퍼티
    public PlayerSO Data { get => playerData; }
    public GameObject Weapon {get => weapon;}
    public PlayerWeapon PlayerWeapon { get; set; }
    

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        Input = GetComponent<PlayerController>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Condition = GetComponent<PlayerCondition>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        PlayerWeapon = Weapon.GetComponent<PlayerWeapon>();
        AnimationDB.Initailize();
        StateMachine = new PlayerStateMachine(this);

        GetComponent<PlayerCondition>().OnDie += OnDie;
    }

    private void Update()
    {
        StateMachine.HandleInput();
        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //사다리 판단
        if (other.CompareTag(StringNameSpace.Tags.Ladder))
        {
            IsLadder = true;
        }
        
        //세이브 포인트
        if (other.CompareTag(StringNameSpace.Tags.SavePoint))
        {
            Input.IsNearSave = true;
            SavePoint dummy = other.GetComponent<SavePoint>();
            Input.NearSavePos = dummy.ReturnPos();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //사다리 나가기
        if (other.CompareTag(StringNameSpace.Tags.Ladder))
        {
            IsLadder = false;
        }
        
        //세이브 포인트 나가기
        if (other.CompareTag(StringNameSpace.Tags.SavePoint))
        {
            Input.IsNearSave = false;
            Input.NearSavePos = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //공중 발판 정보 가져오기
        if (other.gameObject.CompareTag(StringNameSpace.Tags.AerialPlatform))
        {
            AerialPlatform = other.gameObject.GetComponent<AerialPlatform>();
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        //공중 발판 정보 초기화
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

    //공중 발판 확인
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
        DebugHelper.Log("OnDie 호출됨");

        Condition.ChangeInvincible(true);
        Animator.SetTrigger(AnimationDB.DieParameterHash);
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
        StateMachine.ChangeState(StateMachine.IdleState);
        Animator.Rebind();

        Input.enabled = true;
        yield return new WaitForSeconds(0.5f);
    
        Condition.ChangeInvincible(false);
    }

    public void ApplyAttackForce()
    {
        AttackInfo attackInfo = Data.attacks.GetAttackInfo(StateMachine.comboIndex);
        Vector2 direction = SpriteRenderer.flipX ? Vector2.left : Vector2.right;
        Rigidbody.AddForce(direction * attackInfo.attackForce, ForceMode2D.Impulse);
    }
    
    
    #endregion
}
