using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 장착된 장비를 보여주는 UI
/// </summary>
public class EquipUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private string defaultText;
    
    public Image icon;
    public TextMeshProUGUI desc;
    public CollectObjectSO data;
    public Action OnSelect;
    public Action OnEquip;
    
    //장비 장착 시
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
    
    //클릭 이벤트 추가
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!data) return;
        //좌클릭 시
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnSelect.Invoke();
        }
        //우클릭 시
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnEquip.Invoke();
        }
            
    }
}
