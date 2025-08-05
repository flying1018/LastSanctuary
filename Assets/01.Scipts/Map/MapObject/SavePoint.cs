using System.Collections;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    private int _index;
    private bool _isInteracted;
    private Player _player;
    private Coroutine _interactCoroutine;
    private Coroutine _bellCoroutine;
    private Coroutine _effectCoroutine;
    private bool _isEffectOn;
    private BoxCollider2D _boxCollider;

    [Header("interact Animation")]
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform rightPosition;
    [SerializeField] private GameObject rope;
    [SerializeField] private float enterTime;
    [SerializeField] private GameObject bell;
    [SerializeField] private float ropePosition;
    [SerializeField] private float bellRotateAngle;
    [SerializeField] private float bellRotateSpeed;
    [SerializeField] private SpriteRenderer effect;
    [SerializeField] private float targetAlpha;
    
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            if (other.TryGetComponent(out Player player))
            {
                _player = player;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            _player = null;
        }
    }

    public Transform NearPosition()
    {
        float left = Vector3.Distance(leftPosition.position, _player.transform.position);
        float right = Vector3.Distance(rightPosition.position, _player.transform.position);
        
        if (left < right)
        {
            return leftPosition;
        }
        else
        {
            return rightPosition;
        }
    }

    private Vector2 SavePosition()
    {
        return new Vector2(transform.position.x, transform.position.y - _boxCollider.size.y/2);
    }

    public void Interact()
    {
        if (_isInteracted) { return; }

        UIManager.Instance.saveUI.SaveAnimation();

        SaveManager.Instance.SetSavePoint(SavePosition());
        ItemManager.Instance.playerCondition.PlayerRecovery(); // 회복
        ItemManager.Instance.playerInventory.SupplyPotion();
        MapManager.Instance.RespawnEnemies();
        //MapManager.Instance.RespawnItems();
        GetComponent<TutorialUIInterction>()?.ShowUI(); //상호작용시 UI 호출

        //코루틴 초기화
        if (_interactCoroutine != null)
        {
            StopCoroutine(_interactCoroutine);
            _interactCoroutine = null;
        }
        //코루틴 시작
        _interactCoroutine = StartCoroutine(Save_Coroutine());
    }

    private IEnumerator Save_Coroutine()
    {
        //초기 설정
        rope.transform.localPosition = Vector3.zero;
        bell.transform.rotation = Quaternion.identity;
        
        yield return new WaitForSeconds(enterTime);
        
        while (rope.transform.localPosition.y > ropePosition)
        {
            rope.transform.position += Vector3.down * Time.deltaTime;
            yield return null;
        }
        
        while (rope.transform.localPosition.y < 0)
        {
            rope.transform.position += Vector3.up * Time.deltaTime;
            yield return null;
        }

        //코루틴 초기화
        if (_bellCoroutine != null)
        {
            StopCoroutine(_bellCoroutine);
            _bellCoroutine = null;
        }

        if (_effectCoroutine != null)
        {
            StopCoroutine(_effectCoroutine);
            _effectCoroutine = null;
        }
        
        //코루틴 시작
        _bellCoroutine = StartCoroutine(ShakeBell_Coroutine());
        _effectCoroutine = StartCoroutine(Effect_Coroutine());

        _interactCoroutine = null;
    }

    IEnumerator Effect_Coroutine()
    {
        if (_isEffectOn) { yield break; }
        
        Color color = effect.color;
        
        while(targetAlpha > effect.color.a)
        {
            color.a += Time.deltaTime * targetAlpha;
            effect.color = color;
            yield return null;
        }

        _isEffectOn = true;
    }

    IEnumerator ShakeBell_Coroutine()
    {
        while (bell.transform.rotation.z < Quaternion.Euler(0,0,bellRotateAngle).z)
        {
            RotateBell(bellRotateAngle, bellRotateSpeed);
            yield return null;       
        }

        while (bell.transform.rotation.z > Quaternion.Euler(0,0,-bellRotateAngle).z)
        {
            RotateBell(-bellRotateAngle, bellRotateSpeed);
            yield return null;
        }
        
        while (bell.transform.rotation.z < Quaternion.Euler(0,0,bellRotateAngle).z)
        {
            RotateBell(bellRotateAngle, bellRotateSpeed);
            yield return null;       
        }
        
        while (bell.transform.rotation.z > Quaternion.Euler(0,0,0).z)
        {
            RotateBell(-bellRotateAngle, bellRotateSpeed);
            yield return null;
        }

        _bellCoroutine = null;
    }

    private void RotateBell(float angle, float speed)
    {
        bell.transform.rotation *= Quaternion.Euler(0, 0, angle * Time.deltaTime * speed);
    }
}