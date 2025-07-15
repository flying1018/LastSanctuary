using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu (fileName = "UI", menuName = "new UI")]
public class UIManagerSO : ScriptableObject
{
    [Header("RelicUI")]
    public string[] statNames; 
    public int equipNum;    //장비칸 수
    public GameObject statUIPrefab;
    public GameObject equipUIPrefab;
    public GameObject slotUIPrefab;

    [Header("MainUI")] 
    public int buffUINum;
    public int conditionSize;
    public GameObject buffUIPrefab;
    public Sprite potionIcon;
    public Sprite emptyPotionIcon;
}
