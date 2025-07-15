using UnityEngine;

/// <summary>
/// 효과음 실행하는 프리펩
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

        Destroy(gameObject, clip.length + 0.1f);
    }
}