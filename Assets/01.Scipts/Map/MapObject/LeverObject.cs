using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum LeverType
{
    Oneway,
    Toggle,
    
}

public class LeverObject : MonoBehaviour, IDamageable
{
    [SerializeField] private bool isOn = false;
    [SerializeField] private LeverType type;
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
        switch (type)
        {
            case LeverType.Oneway:
                if (isOn) return;
                isOn = true;
                break;
            case LeverType.Toggle:
                isOn = !isOn;
                break;
        }
        
        SoundManager.Instance.PlaySFX(audioClip);
        
        spriteRenderer.sprite = isOn ? leverImage[1] : leverImage[0];

        foreach (MoveObject moveObject in moveObjects)
        {
            if (isOn)
                moveObject.MoveObj();
            else
            {
                moveObject.ReturnObj();
            }
        }

    }

    public void ReturnLever()
    {
        SoundManager.Instance.PlaySFX(audioClip);
        isOn = false;
        spriteRenderer.sprite = leverImage[0];
    }

}