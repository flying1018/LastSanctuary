using UnityEngine;

public class CollectObject : MonoBehaviour, IInteractable
{
    [SerializeField] private CollectObjectSO collectData;
    public bool isGet;
    public bool IsGet { get => isGet; set => isGet = value; }

    public void Interact()
    {
        if (isGet) { return; }

        // ItemManager.Instance.GetCollectItem(collectData);
        // ItemManager.Instance.AddInventory(필요한 파라미터);
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
}