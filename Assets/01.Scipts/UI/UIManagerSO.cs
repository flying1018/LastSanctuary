using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// UI의 필요한 데이터
/// </summary>
[CreateAssetMenu(fileName = "UI", menuName = "new UI")]
public class UIManagerSO : ScriptableObject
{
    [Header("RelicUI")]
    public string[] statNames;
    public int equipNum;    //장비칸 수
    public int nonLockEquip;
    public GameObject statUIPrefab;
    public GameObject equipUIPrefab;
    public GameObject slotUIPrefab;
    public string relicLeftClickDesc;
    public string relicRightClickDesc;

    [Header("MainUI")]
    public int buffUINum;
    public int conditionSize;
    public GameObject buffUIPrefab;
    public Sprite potionIcon;
    public Sprite emptyPotionIcon;

    [Header("SkillUI")]
    public float centerLinePosition;
    public string skillLeftClickDesc;
    public string sKillRightClickDesc;

    [Header("ProduceUI")]
    public GameObject saveUIObj;
    public GameObject itemTextUI;
    
    [Header("Sound")]
    public AudioClip buttonClickSound;
    public AudioClip equipSound;
}
