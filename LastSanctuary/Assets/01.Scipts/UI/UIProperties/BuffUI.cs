using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffUI : MonoBehaviour
{
    [SerializeField] private float blinkTime;
    [SerializeField] private float blickInterval;
    
    public Image icon;
    public StatObjectSO data;
    
    public void SetBuff(StatObjectSO data)
    {
        StartCoroutine(SetBuff_Coroutine(data));
    }

    IEnumerator SetBuff_Coroutine(StatObjectSO data)
    {
        this.data = data;
        icon.gameObject.SetActive(true);
        icon.sprite = data.icon;
        float timer = data.duration;
        bool isVisible = true;
        float blinkTimer = 0f;
        
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;

            //
            if (timer <= blinkTime)
            {
                blinkTimer += Time.deltaTime;
                if (blinkTimer >= blickInterval)
                {
                    blinkTimer = 0f;
                    isVisible = !isVisible;
                    icon.color = new Color(1, 1, 1, isVisible ? 1 : 0);
                }
            }
        }

        icon.gameObject.SetActive(false);
        this.data = null;
    }
}
