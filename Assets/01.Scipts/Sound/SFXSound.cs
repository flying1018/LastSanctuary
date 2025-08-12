using System.Collections;
using System.Threading;
using UnityEngine;

/// <summary>
/// 효과음 실행하는 클래스
/// </summary>
public class SFXSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    
    private Coroutine _coroutine;

    public void Play(AudioClip clip, float volume)
    {
        if (clip == null) return;

        audioSource.volume = Mathf.Clamp01(volume);
        audioSource.clip = clip;
        audioSource.Play();

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = StartCoroutine(ReturnSFX(clip.length + 1f));
    }

    
    private IEnumerator ReturnSFX(float value)
    {   
        yield return new WaitForSeconds(value);
        _coroutine = null;
        ObjectPoolManager.Set(gameObject,(int)PoolingIndex.SFX);
    }
}