using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFadeUI : MonoBehaviour
{
    public Image fadeImage;

    private void Awake()
    {

    }

    public IEnumerator Fade_Coroutine(float duration = 1f, Color? color = null)
    {
        if (color.HasValue) fadeImage.color = color.Value;
        Color c = fadeImage.color;
        c.a = 0;
        fadeImage.color = c;

        float time = 0f;
        while (time < duration / 2)
        {
            c.a = Mathf.Lerp(0, 1, time / (duration / 2));
            fadeImage.color = c;
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        c.a = 1;
        fadeImage.color = c;

        yield return new WaitForSecondsRealtime(0.5f);

        time = 0f;
        while (time < duration / 2)
        {
            c.a = Mathf.Lerp(1, 0, time / (duration / 2));
            fadeImage.color = c;
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        c.a = 0;
        fadeImage.color = c;

        this.gameObject.SetActive(false);
    }
}
