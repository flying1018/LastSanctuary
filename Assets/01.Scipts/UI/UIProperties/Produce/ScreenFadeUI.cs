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

    public IEnumerator Fade_Coroutine(float duration = 1f, Color? color = null)
    {
        if (color.HasValue) _fadeImage.color = color.Value;
        Color c = _fadeImage.color;
        c.a = 0;
        _fadeImage.color = c;

        float time = 0f;
        while (time < duration / 2)
        {
            c.a = Mathf.Lerp(0, 1, time / (duration / 2));
            _fadeImage.color = c;
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        c.a = 1;
        _fadeImage.color = c;

        yield return new WaitForSeconds(0.5f);

        time = 0f;
        while (time < duration / 2)
        {
            c.a = Mathf.Lerp(1, 0, time / (duration / 2));
            _fadeImage.color = c;
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        c.a = 0;
        _fadeImage.color = c;

        this.gameObject.SetActive(false);
    }

    public void FadeOut(float duration = 1f, Color? color = null)
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }
        _fadeCoroutine = StartCoroutine(FadeOut_Coroutine(duration, color));
    }
    
    IEnumerator FadeOut_Coroutine(float duration = 1f, Color? color = null)
    {
        gameObject.SetActive(true); 
        
        Color c = _fadeImage.color;
        float time = 0f;
        
        while (time < duration)
        {
            c.a = Mathf.Lerp(1, 0, time /duration);
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
