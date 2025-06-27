using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private Vector3 _lastSavePos;

    public void SetSavePoint(Vector3 pos)
    {
        _lastSavePos = pos;
    }

    public Vector3 GetSavePoint()
    {
        return _lastSavePos;
    }
}
