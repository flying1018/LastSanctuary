using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKinematicMove : PlayerKinematicMove
{
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float platformCheckDistance = 2f;
    
    public override void AddForce(Vector2 force, float dumping = 0.95f)
    {
        if (AddForceCoroutine != null)
        {
            StopCoroutine(AddForceCoroutine);
            AddForceCoroutine = null;
        }

        AddForceCoroutine = StartCoroutine(EnemyAddForce_Coroutine(force, dumping));
    }

    private IEnumerator EnemyAddForce_Coroutine(Vector2 force, float dumping)
    {
        while (force.magnitude > 0.01f)
        {
            Vector2 nextPosition = _rigidbody.position + force * Time.fixedDeltaTime;

            if (IsOnPlatform(nextPosition))
            {
                Move(force);
            }
            else
            {
                break;
            }

            yield return new WaitForFixedUpdate();
            force *= dumping;
        }

        AddForceCoroutine = null;
    }
    
    public bool IsOnPlatform(Vector2 nextPosition)
    {
      
        float offsetX = (nextPosition.x - _rigidbody.position.x) > 0 ? SizeX: -SizeX;
        
        Vector2 newPos = new Vector2(transform.position.x + offsetX, transform.position.y);
        return Physics2D.Raycast(newPos, Vector2.down,
            platformCheckDistance, platformLayer);
    }
}
