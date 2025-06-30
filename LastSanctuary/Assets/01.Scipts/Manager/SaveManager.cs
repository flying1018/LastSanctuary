using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private Vector3 _lastSavePos;

    public void SetSavePoint(Vector3 pos)
    {
        _lastSavePos = pos;
        DebugHelper.Log($"새로운 세이브 {_lastSavePos}");
    }

    public Vector3 GetSavePoint()
    {
        return _lastSavePos;
    }
}
