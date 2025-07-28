using System.Collections;
using UnityEngine;

/// <summary>
/// 효과음 실행하는 클래스
/// </summary>
public class SFXSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void Play(AudioClip clip, float volume)
    {
        if (clip == null) return;

        audioSource.volume = Mathf.Clamp01(volume);
        audioSource.clip = clip;
        audioSource.Play();

        StartCoroutine(ReturnSFX(clip.length + 1f));
    }

    private IEnumerator ReturnSFX(float value)
    {
        yield return new WaitForSeconds(value);

        ObjectPoolManager.Set(gameObject,(int)ObjectPoolManager.PoolingIndex.SFX);
    }
}