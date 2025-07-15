using UnityEngine;

/// <summary>
/// 배경음악을 AudioSource에서 관리하는 클래스 
/// </summary>
public class BGMSound : MonoBehaviour
{
    private AudioSource audioSource;

    public void Init(AudioSource source)
    {
        audioSource = source;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        DontDestroyOnLoad(gameObject); 
    }

    public void Play(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("오디오 클립 null");
            return;
        }

        if (audioSource.clip == clip && audioSource.isPlaying)
        {
            Debug.Log("중복된 BGM");
            return;
        }

        if (audioSource.isPlaying)
        {
            Stop();
        }

        audioSource.clip = clip;
        audioSource.Play();
        Debug.Log($"BGM 재생: {clip.name}");
    }

    public void Stop()
    {
        audioSource?.Stop();
    }
}