using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public enum MoveObjType
    {
        None,
        IronCage,
        Rope,
    }

    [SerializeField] private MoveObjType moveObjType;

    private bool _isTurnOn;

    public void MoveObj()
    {

        switch (moveObjType)
        {
            case MoveObjType.None:
                DebugHelper.LogError("MoveObjType 현재 None임 확인 요망");
                break;

            case MoveObjType.IronCage:
                StartCoroutine(MoveIronCage(_isTurnOn));
                _isTurnOn = !_isTurnOn;
                break;

            case MoveObjType.Rope:
                break;
        }
    }

    private IEnumerator MoveIronCage(bool _isTurnOn)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(startPos.x, _isTurnOn ? transform.position.y + -3.6f : transform.position.y + 3.6f, startPos.z);
        float elapsed = 0f;

        while (elapsed < 3.6f)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }
}
