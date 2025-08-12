using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 인벤토리를 보여주는 UI
/// </summary>
public class SlotUI : MonoBehaviour,IPointerClickHandler
{
    public Image icon;
    public CollectObject data;
    public Action OnSelect;
    public Action OnEquip;

    //아이템 습득 시
    public void SetActive()
    {
        icon.sprite = data.Data.relicSprite;
        icon.gameObject.SetActive(data.IsGet);
    }

    //클릭 이벤트
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
