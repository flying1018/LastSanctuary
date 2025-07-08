using System;
using Unity.VisualScripting;
using UnityEngine;

public class StatObject : MonoBehaviour, IInteractable
{
    public event Action OnInteracte;
    
    [SerializeField] private StatObjectSO statData;
    
    public bool isGet;
    public bool IsGet { get => isGet; set => isGet = value; }

    public void Interact()
    {
        if (isGet) { return; }
        
        isGet = true;
        ItemManager.Instance.UpgradeStat(statData);
        OnInteracte?.Invoke();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// isGet 유무에 따라 해당 오브젝트의 활성화 / 비활성화
    /// </summary>
    public void SetActive()
    {
        gameObject.SetActive(!isGet);
    }
}
