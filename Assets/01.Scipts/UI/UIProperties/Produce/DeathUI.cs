
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private string defaultText = "";

    Coroutine _routine;

    void Awake()
    {
        gameObject.SetActive(true);
    }

    public void ShowForSeconds(float totalSeconds)
    {
        if (_routine != null) StopCoroutine(_routine);
        _routine = StartCoroutine(ShowForSeconds_Co(totalSeconds, defaultText));
    }

    IEnumerator ShowForSeconds_Co(float totalSeconds, string text)
    {
        message.text = text;

        Color c = message.color;

        // 첫 절반: 알파 0 → 1
        float halfTime = totalSeconds * 0.5f;
        float t = 0f;
        while (t < halfTime)
        {
            t += Time.unscaledDeltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / halfTime);
            message.color = c;
            yield return null;
        }
        c.a = 1f;
        message.color = c;

        // 둘째 절반: 알파 1 → 0
        t = 0f;
        while (t < halfTime)
        {
            t += Time.unscaledDeltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / halfTime);
            message.color = c;
            yield return null;
        }
        c.a = 0f;
        message.color = c;
        _routine = null;
        this.gameObject.SetActive(false);
    }
}
