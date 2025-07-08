using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour,IPointerClickHandler
{
    public Image icon;
    public CollectObject data;
    public bool isSelected;
    public bool isEquip;
    public Action OnSelect;
    public Action OnEquip;

    public void SetActive()
    {
        icon.gameObject.SetActive(data.IsGet);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
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
