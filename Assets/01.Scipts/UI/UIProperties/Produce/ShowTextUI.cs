
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI message;

    protected Coroutine _routine;

    protected void Awake()
    {
        gameObject.SetActive(true);
    }

    public virtual void ShowText(float totalSeconds, string text = null)
    {
        if (_routine != null) StopCoroutine(_routine);
        _routine = StartCoroutine(ShowText_Coroutine(totalSeconds, text));
    }

   protected IEnumerator ShowText_Coroutine(float fadeTime, string text)
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
