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
        SoundManager.Instance.PlaySFX(statData.getSound);

        string itemName;
        if (statData.isConsumable)
        {
            itemName = "버프 획득";
        }
        else
        {
            itemName = "+" + statData.statDeltas[0].statType.ToString() + " " + statData.statDeltas[0].amount.ToString();
        }

        UIManager.Instance.ShowItemText(itemName, transform.position + Vector3.up * 1.5f);

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
