using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] private Image hp;
    [SerializeField] private Image groggy;
    
    public BossCondition BossCondition{ get; set; }
    
    public void Init()
    {
        gameObject.SetActive(false);
    }

    public void Update()
    {
        //UI 갱신
        hp.fillAmount = BossCondition.HpValue();
        groggy.fillAmount = BossCondition.GroggyGaugeValue();
    }
}
