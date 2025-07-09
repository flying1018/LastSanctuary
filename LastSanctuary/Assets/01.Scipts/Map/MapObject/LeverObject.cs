using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverObject : MonoBehaviour, IDamageable
{
    [SerializeField] private bool isOn = false;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip audioClip;

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
        UpdateVisual();

        yield return new WaitForSeconds(2f);

        _isMove = false;
    }
}
