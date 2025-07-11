using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BossDirecting : MonoBehaviour
{
    [Header("카메라")]
    public CinemachineVirtualCamera PlayerCamera;
    public CinemachineVirtualCamera BossCamera;
    
    [Header("연출 시간")]
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float slowScale = 0.5f;
    [SerializeField] private float slowTimeScale = 0.2f;

    public void PlayCutscene()
    {
        StartCoroutine(CutsceneCoroutine());
    }

    private IEnumerator CutsceneCoroutine()
    {
        BossCamera.Priority = 20;
        
        Time.timeScale = slowTimeScale;
        yield return new WaitForSeconds(fadeDuration);
    }
}
