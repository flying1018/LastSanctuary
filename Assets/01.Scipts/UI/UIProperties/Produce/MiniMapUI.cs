using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapUI : MonoBehaviour
{
    [System.Serializable]
    public struct MapData
    {
        public MapType type;
        public GameObject[] miniMaps;
    }

    [SerializeField] private MapData[] maps;
    private RawImage _curMiniMap;
    private MapType _curMapType;

    private bool _isOn = false;

    public void ShowMiniMap()
    {
        _isOn = !_isOn;

        _curMapType = MapManager.Instance.Map;

        DebugHelper.Log($"호출됨 : {_isOn}");

        if (_isOn)
        {
            maps[(int)_curMapType].miniMaps[0].SetActive(true);
            // foreach (var map in maps)
            //     map.canvas.SetActive(map.type == curMapType);
        }
        else
        {
            // foreach (var map in maps)
            //     map.canvas.SetActive(false);

            this.gameObject.SetActive(false);
        }


    }
}
