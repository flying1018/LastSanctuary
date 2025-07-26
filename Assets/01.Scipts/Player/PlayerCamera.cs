using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Player _player;
    private CinemachineVirtualCamera _camera;
    private CinemachineFramingTransposer _transposer; 
    
    public void Init(Player player)
    {
        _player = player;
        _camera = GetComponent<CinemachineVirtualCamera>();
        _transposer = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void RotateCamera(Vector2 direction)
    {
        if (direction.x != 0)
        {
            _transposer.m_TrackedObjectOffset.x = direction.x > 0 ? _player.Data.cameraDiff : -_player.Data.cameraDiff;
        }
    }
    
    
    
    
    
}
