using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeObject : MoveObject
{
    [SerializeField] private int ropeLength = 1;
    [SerializeField] private GameObject baseRope;
    [SerializeField] private GameObject ropeSegmentPrefab;

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
            Instantiate(ropeSegmentPrefab, transform.position + Vector3.down * i, Quaternion.identity, transform);
        }
    }
}

