using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronCage : MoveObject
{
    [SerializeField] private float moveDistance = 3.6f;

    public void Awake()
    {
        _isTurnOn = false;
    }

    public override void MoveObj()
    {
        _isTurnOn = !_isTurnOn;
        StartCoroutine(MoveIronCage(base._isTurnOn));
    }

    private IEnumerator MoveIronCage(bool _isTurnOn)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (!_isTurnOn) { spriteRenderer.sortingOrder = 0; }

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

        if (_isTurnOn) { spriteRenderer.sortingOrder = 50; }
        transform.position = targetPos;
    }
}