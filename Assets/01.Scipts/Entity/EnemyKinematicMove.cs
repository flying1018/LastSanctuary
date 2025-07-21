using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKinematicMove : KinematicMove
{
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float platformCheckDistance = 0.2f;
    
    // public override void Move(Vector2 direction)
    // {
    //     Vector2 horizontal = direction;
    //     Vector2 vertical = direction;
    //
    //     horizontal.y = 0;
    //     vertical.x = 0;
    //     
    //     if (IsWall && WallDirection.normalized == horizontal.normalized) direction.x = 0;
    //     if (IsGrounded && GroundDirection.normalized == vertical.normalized) direction.y = 0;
    //     
    //     if(IsAerialPlatform && !IsOnPlatform(horizontal)) direction.x = 0;
    //     
    //     _rigidbody.MovePosition(_rigidbody.position + direction * Time.fixedDeltaTime);
    // }

    public override void AddForce(Vector2 force, float dumping = 0.95f)
    {
        if (addForceCoroutine != null)
        {
            StopCoroutine(addForceCoroutine);
            addForceCoroutine = null;
        }

        addForceCoroutine = StartCoroutine(EnemyAddForce_Coroutine(force, dumping));
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
                break; // 플랫폼 없으면 중단
            }

            yield return null;
            force *= dumping;
        }

        addForceCoroutine = null;
    }
    
    public bool IsOnPlatform(Vector2 direction)
    {
        // float offsetX = direction.x > 0 ? SizeX: -SizeX;
        //
        // Vector2 newPos = new Vector2(transform.position.x + offsetX, transform.position.y);
        // Debug.DrawRay(newPos, Vector2.down * platformCheckDistance, Color.red, 0.1f);
        // return Physics2D.Raycast(newPos, Vector2.down,
        //     platformCheckDistance, platformLayer);
    
        RaycastHit2D hit = Physics2D.BoxCast(direction, new Vector2(SizeX, SizeY), 0, Vector2.down, 0.1f, LayerMask.GetMask("Ground", "AerialPlatform"));
        return hit.collider != null;
        
    }
}
