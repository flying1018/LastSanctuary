using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverObject : MonoBehaviour, IDamageable
{
    [SerializeField] private bool isOn = false;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private MoveObject[] _moveObjects;

    private bool _isMove;

    private void UpdateVisual()
    {
        animator.SetBool("isOn", isOn);
    }

    public void TakeDamage(int atk, DamageType type, Transform attacker, float defpen)
    {
        if (_isMove) { return; }
        StartCoroutine(Toggle());
    }

    private IEnumerator Toggle()
    {
        //SoundManager.Instance.PlaySFX(audioClip);
        _isMove = true;

        isOn = !isOn;
        DebugHelper.Log($"레버 상태 : {isOn}임");

        foreach (MoveObject _moveObject in _moveObjects)
        {
            _moveObject.MoveObj();
        }
        UpdateVisual();

        yield return new WaitForSeconds(2f);

        _isMove = false;
    }
}
