using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Player _player;
    private CinemachineVirtualCamera _camera;
    private CinemachineFramingTransposer _transposer;
    
    [SerializeField] private CinemachineVirtualCamera otherCam;

    private float _defaultOrthoSize;

    //프로퍼티
    public bool StopCamera { get; set; }

    public void Init(Player player)
    {
        _player = player;
        _camera = GetComponent<CinemachineVirtualCamera>();
        _transposer = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        _defaultOrthoSize = _camera.m_Lens.OrthographicSize;
    }

    public void RotateCamera(Vector2 direction)
    {
        if (StopCamera || _transposer == null) return;
        if (direction.x != 0)
            _transposer.m_TrackedObjectOffset.x = direction.x > 0 ? _player.Data.cameraDiff : -_player.Data.cameraDiff;
    
        _transposer.m_TrackedObjectOffset.y = direction.y >= 0 ? _player.Data.cameraTopView : -_player.Data.cameraBottomView;
            
        
    }

    /// <summary>
    /// 전투 시작시 
    /// </summary>
    /// <param name="focusPosition"></param>
    /// <param name="zoom"></param>
    public void StartBattleCamera(Vector2 focusPosition, float zoom = 5f)
    {
        if (otherCam == null) return;
        otherCam.Priority = 20;
        otherCam.Follow = null;
        otherCam.transform.position = new Vector3(focusPosition.x, focusPosition.y, otherCam.transform.position.z);
        otherCam.m_Lens.OrthographicSize = zoom;
    }


    public void EndBattleCamera()
    {
        if (otherCam == null) return;
        otherCam.Priority = 10;
        _camera.Priority = 20;
        _camera.Follow = _player.transform;
        _camera.m_Lens.OrthographicSize = _defaultOrthoSize;
    }

    /// <summary>
    /// 카메라 흔들림
    /// </summary>
    /// <param name="amplitude"></param>
    /// <param name="frequency"></param>
    /// <param name="duration"></param>
    public void ShakeCamera(float amplitude = 1f, float frequency = 1f, float duration = 1f)
    {
        var perlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (perlin == null) return;

        perlin.m_AmplitudeGain = amplitude;
        perlin.m_FrequencyGain = frequency;
        StartCoroutine(StopShake(perlin, duration));
    }

    private IEnumerator StopShake(CinemachineBasicMultiChannelPerlin perlin, float duration)
    {
        yield return new WaitForSeconds(duration);
        perlin.m_AmplitudeGain = 0f;
        perlin.m_FrequencyGain = 0f;
    }

    /// <summary>
    /// 컷씬에 사용되는 카메라
    /// </summary>
    /// <param name="target"></param>
    /// <param name="zoom"></param>
    public void StartZoomCamera(Transform target, float zoom = 6f)
    {
        if (otherCam == null) return;
        otherCam.Priority = 20;
        otherCam.Follow = target;
        otherCam.m_Lens.OrthographicSize = zoom;
    }
    public void EndZoomCamera()
    {
        if (otherCam == null) return;
        otherCam.Priority = 10;
        _camera.Priority = 20;
        _camera.Follow = _player.transform;
        _camera.m_Lens.OrthographicSize = _defaultOrthoSize;
    }
}
