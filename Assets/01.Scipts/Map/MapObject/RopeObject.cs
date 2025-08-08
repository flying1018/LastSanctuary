using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class RopeObject : MoveObject
{
    [SerializeField] private int ropeLength = 1;
    [SerializeField] private GameObject baseRope;
    [SerializeField] private GameObject middleRopePrefab;
    [SerializeField] private GameObject topRopePrefab;
    [SerializeField] private GameObject bottomRopePrefab;

    [SerializeField] private GameObject coli;
    private bool _isActived = false;


    public override void MoveObj()
    {
        MoveRope();
    }
    private void MoveRope()
    {
        if (_isActived) { return; }

        _isActived = true;
        baseRope.SetActive(false);
        coli.SetActive(true);

        for (int i = 0; i < ropeLength; i++)
        {
            if (i == 0)
            {
                Instantiate(topRopePrefab, transform.position + Vector3.down * i, Quaternion.identity, transform);
                continue;
            }
            else if (i == ropeLength - 1)
            {
                Instantiate(bottomRopePrefab, transform.position + Vector3.down * i, Quaternion.identity, transform);
                continue;
            }
            Instantiate(middleRopePrefab, transform.position + Vector3.down * i, Quaternion.identity, transform);
        }

        coli.transform.localPosition = Vector3.down * (ropeLength / 2);
        coli.transform.localScale = new Vector3(1, ropeLength, 1);
    }
}

