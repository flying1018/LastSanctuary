using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFadeUI : MonoBehaviour
{
    public Image fadeImage;
    private Coroutine _coroutine;

    public void FadeBackground(float duration, float holdSeconds = 0f, Color? color = null)
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        gameObject.SetActive(true);
        _coroutine = StartCoroutine(Fade_Coroutine(duration, holdSeconds, color));
    }

    public IEnumerator Fade_Coroutine(float duration, float holdSeconds = 0f, Color? color = null)
    {
        if (color.HasValue) fadeImage.color = color.Value;

        // 알파 0으로 시작
        Color c = fadeImage.color; c.a = 0f; fadeImage.color = c;

        float half = Mathf.Max(0.0001f, duration * 0.5f);

        // IN
        float t = 0f;
        while (t < half)
        {
            t += Time.unscaledDeltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / half);
            fadeImage.color = c;
            yield return null;
        }
        c.a = 1f; fadeImage.color = c;

        // HOLD (옵션)
        if (holdSeconds > 0f)
            yield return new WaitForSecondsRealtime(holdSeconds);

        // OUT
        t = 0f;
        while (t < half)
        {
            t += Time.unscaledDeltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / half);
            fadeImage.color = c;
            yield return null;
        }
        c.a = 0f; fadeImage.color = c;

        _coroutine = null;
        gameObject.SetActive(false);
    }
}
