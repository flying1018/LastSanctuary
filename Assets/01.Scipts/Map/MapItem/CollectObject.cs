using System;
using UnityEngine;

public class CollectObject : MonoBehaviour, IInteractable, IComparable<CollectObject>
{
    [SerializeField] private CollectObjectSO collectData;
    
    public bool isGet;
    
    //프로퍼티
    public CollectObjectSO Data { get => collectData; }
    public bool IsGet { get => isGet; set => isGet = value; }

    //충돌한 플레이어의 PlayerInventory 정보를 받아서 처리해도 될듯.
    public void Interact()
    {
        if (isGet) { return; }

        ItemManager.Instance.GetCollectItem(collectData);
        isGet = true;

        SetActive();
    }

    /// <summary>
    /// isGet 유무에 따라 해당 오브젝트의 활성화 / 비활성화
    /// </summary>
    public void SetActive()
    {
        gameObject.SetActive(!isGet);
    }

    public int CompareTo(CollectObject other)
    {
        return collectData.index.CompareTo(other.collectData.index);
    }
}