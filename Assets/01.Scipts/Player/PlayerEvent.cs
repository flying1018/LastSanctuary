using System.Collections;
using Cinemachine;
using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera defaultCam;
    [SerializeField] private CinemachineVirtualCamera battleCam;
    [SerializeField] private CinemachineVirtualCamera cutsceneCam;

    private Player _player;
    private float _defaultOrthoSize;

    private void Awake()
    {
        _player = GetComponent<Player>();
        if (defaultCam != null)
            _defaultOrthoSize = defaultCam.m_Lens.OrthographicSize;
    }

    /// <summary>
    /// 전투 시작시 카메라 연출
    /// </summary>
    /// <param name="focusPosition"></param>
    /// <param name="zoom"></param>
    public void StartBattleCamera(Vector2 focusPosition, float zoom = 5f)
    {
        battleCam.Priority = 20;
        battleCam.Follow = null;
        battleCam.transform.position = new Vector3(focusPosition.x, focusPosition.y, battleCam.transform.position.z);
        battleCam.m_Lens.OrthographicSize = zoom;
    }

    /// <summary>
    /// 전투 종료 후 카메라 복구
    /// </summary>
    public void EndBattleCamera()
    {
        battleCam.Priority = 10;
        defaultCam.Priority = 20;
        defaultCam.Follow = _player.transform;
        defaultCam.m_Lens.OrthographicSize = _defaultOrthoSize;
    }

    /// <summary>
    /// 카메라 흔들기
    /// </summary>
    /// <param name="amplitude"></param>
    /// <param name="frequency"></param>
    /// <param name="duration"></param>
    public void ShakeCamera(float amplitude, float frequency, float duration)
    {
        var perlin = defaultCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = amplitude;
        perlin.m_FrequencyGain = frequency;
        StartCoroutine(StopShake(perlin, duration));
    }
    /// <summary>
    /// 카메라 흔들기 종료
    /// </summary>
    /// <param name="perlin"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator StopShake(CinemachineBasicMultiChannelPerlin perlin, float duration)
    {
        yield return new WaitForSeconds(duration);
        perlin.m_AmplitudeGain = 0f;
        perlin.m_FrequencyGain = 0f;
    }

    /// <summary>
    /// 컷씬 전용 카메라
    /// </summary>
    /// <param name="target"></param>
    /// <param name="zoom"></param>
    public void StartCutsceneCamera(Transform target, float zoom = 6f)
    {
        cutsceneCam.Priority = 30;
        cutsceneCam.Follow = target;
        cutsceneCam.m_Lens.OrthographicSize = zoom;
    }

    /// <summary>
    /// 컷씬 종료
    /// </summary>
    public void EndCutsceneCamera()
    {
        cutsceneCam.Priority = 10;
        defaultCam.Priority = 20;
        defaultCam.Follow = _player.transform;
        defaultCam.m_Lens.OrthographicSize = _defaultOrthoSize;
    }
}
