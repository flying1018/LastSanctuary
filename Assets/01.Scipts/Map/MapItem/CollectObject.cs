using System;
using UnityEngine;

public class CollectObject : MonoBehaviour, IInteractable, IComparable<CollectObject>
{
    //필드
    private bool _isGet;

    //직렬화
    [SerializeField] private CollectObjectSO collectData;

    //프로퍼티
    public CollectObjectSO Data { get => collectData; }
    public bool IsGet { get => _isGet; set => _isGet = value; }

    //충돌한 플레이어의 PlayerInventory 정보를 받아서 처리해도 될듯.
    public void Interact()
    {
        if (_isGet) { return; }

        ItemManager.Instance.GetCollectItem(collectData);

        string itemName;
        if (Data.collectType == CollectType.Potion)
        {
            itemName = "체력포션";
        }
        else
        {
            itemName = Data.relicName;
        }
        
        //UIManager.Instance.ShowItemText(itemName, transform.position + Vector3.up * 1.5f);

        _isGet = true;
        GetComponent<TutorialUIInterction>()?.ShowUI(); //상호작용시 UI 호출

        SetActive();
    }

    /// <summary>
    /// isGet 유무에 따라 해당 오브젝트의 활성화 / 비활성화
    /// </summary>
    public void SetActive()
    {
        gameObject.SetActive(!_isGet);
    }

    //수집물 번호 정렬
    public int CompareTo(CollectObject other)
    {
        return collectData.index.CompareTo(other.collectData.index);
    }
}