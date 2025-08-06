using System;
using Unity.VisualScripting;
using UnityEngine;

public class StatObject : MonoBehaviour, IInteractable
{
    public event Action OnInteracte;
    
    [SerializeField] private StatObjectSO statData;
    
    private bool _isGet;
    public bool IsGet { get => _isGet; set => _isGet = value; }

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = statData.icon;
    }


    //상호작용 시
    public void Interact()
    {
        if (_isGet) { return; }
        
        _isGet = true;
        ItemManager.Instance.UpgradeStat(statData);
        GetComponent<TutorialUIInterction>()?.ShowUI(); //상호작용시 UI 호출
        OnInteracte?.Invoke();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// isGet 유무에 따라 해당 오브젝트의 활성화 / 비활성화
    /// </summary>
    public void SetActive()
    {
        gameObject.SetActive(!_isGet);
    }
}
