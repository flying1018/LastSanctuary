using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image icon;
    public CollectObject coll;
    public bool isSelected;
    public bool isEquip;

    public void SetActive()
    {
        icon.gameObject.SetActive(coll.IsGet);
    }
}
