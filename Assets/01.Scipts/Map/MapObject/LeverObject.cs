using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class LeverObject : MonoBehaviour, IDamageable
{
    [SerializeField] private bool isOn = false;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private MoveObject[] moveObjects;
    [SerializeField] private Sprite[] leverImage;

    private SpriteRenderer spriteRenderer;
    private bool _isMove;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator Toggle()
    {
        //SoundManager.Instance.PlaySFX(audioClip);
        _isMove = true;

        isOn = !isOn;

        if (isOn)
        {
            DebugHelper.Log("IsOn true상태");
            spriteRenderer.sprite = leverImage[0];
        }
        else
        {
            DebugHelper.Log("IsOn false상태");
            spriteRenderer.sprite = leverImage[1];
        }

        foreach (MoveObject _moveObject in moveObjects)
        {
            _moveObject.MoveObj();
        }

        yield return new WaitForSeconds(2f);

        _isMove = false;
    }

    public void TakeDamage(WeaponInfo weaponInfo)
    {
        if (_isMove) { return; }
        StartCoroutine(Toggle());
    }
}
