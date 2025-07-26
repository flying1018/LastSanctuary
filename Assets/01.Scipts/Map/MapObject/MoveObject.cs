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

    [SerializeField] private GameObject[] TurnOff;
    [SerializeField] private GameObject[] TurnOn;

    [SerializeField] private float moveDistance = StringNameSpace.ValueSpace.MoveDistance;

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
                if (_isTurnOn) { return; }

                StartCoroutine(MoveRope());
                _isTurnOn = true;
                break;
        }
    }

    private IEnumerator MoveIronCage(bool _isTurnOn)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(!_isTurnOn) { spriteRenderer.sortingOrder = 0; }

        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(
            startPos.x,
            _isTurnOn ? transform.position.y + -moveDistance : transform.position.y + moveDistance,
            startPos.z);

        float elapsed = 0f;

        while (elapsed < 3.6f)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if(_isTurnOn) { spriteRenderer.sortingOrder = 50; }
        transform.position = targetPos;
    }

    private IEnumerator MoveRope()
    {
        foreach (GameObject dummy in TurnOff)
        {
            dummy.SetActive(true);
        }

        foreach (GameObject dummy in TurnOn)
        {
            dummy.SetActive(false);
        }

        yield return null;
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
