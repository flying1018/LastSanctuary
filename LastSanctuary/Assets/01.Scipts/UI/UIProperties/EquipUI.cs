using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipUI : MonoBehaviour
{
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private string defaultText;
    
    
    public Image icon;
    public TextMeshProUGUI desc;
    public CollectObjectSO data;

    public void SetActive()
    {
        if (data == null)
        {
            icon.sprite = defaultSprite;
            desc.text = defaultText;
        }
        else
        {
            icon.sprite = data.relicSprite;
            desc.text = data.effectDesc;
        }
    }
}
