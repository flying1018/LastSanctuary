
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

    public void DeathText(float totalSeconds)
    {
        if (_routine != null) StopCoroutine(_routine);
        _routine = StartCoroutine(DeathText_Coroutine(totalSeconds, defaultText));
    }

    IEnumerator DeathText_Coroutine(float fadeTime, string text)
    {
        message.text = text;

        Color c = message.color;

        float halfTime = fadeTime * 0.5f;
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
