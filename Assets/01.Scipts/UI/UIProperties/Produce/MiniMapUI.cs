using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapUI : MonoBehaviour
{
    [SerializeField] private GameObject[] mapCanvas;
    private int _curMap;
    private bool _isOn = false;

    public void ShowMiniMap()
    {
        _isOn = !_isOn;

        if (_isOn)
        {
            mapCanvas[_curMap - 1].SetActive(true);
        }
        else
        {
            // 미니맵 키고 딴맵가면 오류 방지
            foreach (GameObject canva in mapCanvas)
            {
                canva.SetActive(false);
            }

            this.gameObject.SetActive(false);
        }
    }
}
