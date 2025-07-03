using UnityEngine;

public class StatObject : MonoBehaviour, IInteractable
{
    [SerializeField] private StatObjectSO collectData;
    public bool isGet;
    public bool IsGet { get => isGet; set => isGet = value; }

    public void Interact()
    {
        if (isGet) { return; }

        // ItemManager.Instance.UpgradeStat(_statData);
        isGet = true;

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
