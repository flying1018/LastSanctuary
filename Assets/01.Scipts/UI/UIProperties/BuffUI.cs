using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MainUI에 있는 버프 표시 창
/// </summary>
public class BuffUI : MonoBehaviour
{
    [SerializeField] private float blinkTime;
    [SerializeField] private float blickInterval;
    
    public Image icon;
    public StatObjectSO data;
    
    //버프가 생겼을 시 표시
    public void SetBuff(StatObjectSO data)
    {
        //오브젝트가 껏다 켜질 때 코루틴이 멈추므로 UIManager를 통해서 실행
        UIManager.Instance.StartCoroutine(SetBuff_Coroutine(data));
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
        UIManager.Instance.StateMachine.MainUI.UpdateCondition();
    }
}
