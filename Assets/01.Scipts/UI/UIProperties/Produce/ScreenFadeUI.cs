using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFadeUI : MonoBehaviour
{
    private Image _fadeImage;
    private Coroutine _fadeCoroutine;

    private void Awake()
    {
        _fadeImage = GetComponent<Image>();
    }

    public void FadeBackground(float duration, float holdSeconds = 0f, Color? color = null)
    {
        if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
        gameObject.SetActive(true);
        _fadeCoroutine = StartCoroutine(Fade_Coroutine(duration, holdSeconds, color));
    }

    public IEnumerator Fade_Coroutine(float duration, float holdSeconds = 0f, Color? color = null)
    {
        if (color.HasValue) _fadeImage.color = color.Value;
        Color c = _fadeImage.color;
        c.a = 0;
        _fadeImage.color = c;

        float half = Mathf.Max(0.0001f, duration * 0.5f);

        float time = 0f;
        while (time < half)
        {
            c.a = Mathf.Lerp(0, 1, time / (duration / 2));
            _fadeImage.color = c;
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        c.a = 1;
        _fadeImage.color = c;

        // HOLD (옵션)
        if (holdSeconds > 0f)
            yield return new WaitForSecondsRealtime(holdSeconds);

        // OUT
        time = 0f;
        while (time < half)
        {
            c.a = Mathf.Lerp(1, 0, time / (duration / 2));
            _fadeImage.color = c;
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        c.a = 0;
        _fadeImage.color = c;

        _fadeCoroutine = null;
        gameObject.SetActive(false);
    }

    public void FadeIn(Color color, float startAlpha, float duration)
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }
        _fadeCoroutine = StartCoroutine(FadeIn_Coroutine(color, startAlpha, duration));
    }
    
    IEnumerator FadeIn_Coroutine(Color color, float startAlpha, float duration)
    {
        Color c = color;
        float time = 0f;

        while (time < duration)
        {
            c.a = Mathf.Lerp(startAlpha, 1, time / duration);
            _fadeImage.color = c;
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        c.a = 1;
        _fadeImage.color = c;

        _fadeCoroutine = null;
    }

    public void FadeOut(Color color, float startAlpha, float duration)
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }
        _fadeCoroutine = StartCoroutine(FadeOut_Coroutine(color, startAlpha, duration));
    }

    IEnumerator FadeOut_Coroutine(Color color, float startAlpha, float duration)
    {
        Color c = color;
        float time = 0f;

        while (time < duration)
        {
            c.a = Mathf.Lerp(startAlpha, 0, time / duration);
            _fadeImage.color = c;
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        c.a = 0;
        _fadeImage.color = c;

        _fadeCoroutine = null;

        gameObject.SetActive(false);

    }
}
