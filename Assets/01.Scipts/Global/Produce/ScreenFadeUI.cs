using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFadeUI : MonoBehaviour
{
    public Image fadeImage;

    private void Awake()
    {
        fadeImage.gameObject.SetActive(false);
    }

    public IEnumerator FadeOut_Coroutine(float duration = 1f, Color? color = null)
    {
        if (color.HasValue) fadeImage.color = color.Value;
        Color c = fadeImage.color;
        c.a = 0;
        fadeImage.color = c;
        fadeImage.gameObject.SetActive(true);

        float time = 0f;
        while (time < duration)
        {
            c.a = Mathf.Lerp(0, 1, time / duration);
            fadeImage.color = c;
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        c.a = 1;
        fadeImage.color = c;
    }

    public IEnumerator FadeIn_Coroutine(float duration = 1f)
    {
        Color c = fadeImage.color;
        c.a = 1;
        fadeImage.color = c;
        float time = 0f;
        while (time < duration)
        {
            c.a = Mathf.Lerp(1, 0, time / duration);
            fadeImage.color = c;
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        c.a = 0;
        fadeImage.color = c;
        fadeImage.gameObject.SetActive(false);
    }

    public void FadeOut(float duration = 1f, Color? color = null)
    {
        StartCoroutine(FadeOut_Coroutine(duration, color));
    }

    public void FadeIn(float duration = 1f)
    {
        StartCoroutine(FadeIn_Coroutine(duration));
    }
}
