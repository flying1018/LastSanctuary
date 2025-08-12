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
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI desc;
    [SerializeField] private Image lockImage;
    
    //프로퍼티
    public CollectObjectSO Data { get; set; }
    public Action OnSelect { get; set; }
    public Action OnEquip { get; set; }
    public bool IsLock { get; set; }
    
    //장비 장착 시
    public void SetActive()
    {
        if (Data == null)
        {
            icon.sprite = defaultSprite;
            desc.text = defaultText;
        }
        else
        {
            icon.sprite = Data.relicSprite;
            desc.text = Data.effectDesc;
        }
    }

    public void SetLock(bool isLock)
    {
        IsLock = isLock;
        if (IsLock)
        {
            icon.sprite = lockImage.sprite;
            lockImage.gameObject.SetActive(true);
            desc.text = "";
        }
        else
        {
            icon.sprite = defaultSprite;
            lockImage.gameObject.SetActive(false);
            desc.text = defaultText;
        }
    }
    
    //클릭 이벤트 추가
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!Data) return;
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
