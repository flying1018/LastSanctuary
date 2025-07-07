using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpObject : MonoBehaviour, IInteractable
{
    public int index;
    public bool isActivated;
    public Transform WarpTransform;

    public void Interact()
    {
        if (!isActivated) { isActivated = true; }

        MapManager.Instance.selectWarpObj = this;
        StartCoroutine(WarpAnimation());
    }

    /// <summary>
    /// 애니메이션 연결 필요
    /// </summary>
    /// <returns></returns>
    private IEnumerator WarpAnimation()
    {
        //SoundManager.Instance.PlaySFX(StringNameSpace.SoundAddress.WarpObjectSFX);

        MapManager.Instance.WarpInteract();
        /* 
        추후 WarpInteract에서 UI연결 필요하며
        다른 활성화된 WaroObject를 선택하여 플레이어 위치를 이동해야함
        */
        yield return new WaitForSeconds(2f);
    }
}
