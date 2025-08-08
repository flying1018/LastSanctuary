using System.Collections;
using System.Collections.Generic;
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
    

    public void TakeDamage(WeaponInfo weaponInfo)
    {
        if(_isMove) return;
        
        //SoundManager.Instance.PlaySFX(audioClip);
        
        isOn = true;
        
        if (isOn)
        {
            spriteRenderer.sprite = leverImage[1];
        }
     
        foreach (MoveObject _moveObject in moveObjects)
        {
            _moveObject.MoveObj();
        }
    }

    public void ReturnLever()
    {
        isOn = false;
        spriteRenderer.sprite = leverImage[0];
    }

}
