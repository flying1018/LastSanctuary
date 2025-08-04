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

    private IEnumerator Toggle()
    {
        //SoundManager.Instance.PlaySFX(audioClip);
        _isMove = true;

        isOn = !isOn;

        foreach (MoveObject _moveObject in _moveObjects)
        {
            _moveObject.MoveObj();
        }
        UpdateVisual();

        yield return new WaitForSeconds(2f);

        _isMove = false;
    }

    public void TakeDamage(WeaponInfo weaponInfo)
    {
        if (_isMove) { return; }
        StartCoroutine(Toggle());
    }
}
