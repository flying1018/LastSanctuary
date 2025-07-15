using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private string defaultText;
    
    public Image icon;
    public TextMeshProUGUI desc;
    public CollectObjectSO data;
    public Action OnSelect;
    public Action OnEquip;
    

    public void SetActive()
    {
        if (data == null)
        {
            icon.sprite = defaultSprite;
            desc.text = defaultText;
        }
        else
        {
            icon.sprite = data.relicSprite;
            desc.text = data.effectDesc;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!data) return;
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnSelect.Invoke();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnEquip.Invoke();
        }
            
    }
}
