using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공중 발판 접촉을 추가
/// </summary>
public class PlayerKinematicMove : KinematicMove
{
     protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        
        //공중 발판 충돌 시
        if (other.gameObject.CompareTag(StringNameSpace.Tags.AerialPlatform))
        {
            Vector3 point = other.ClosestPoint(transform.position);
            GroundDirection = point - (transform.position - new Vector3(0,SizeY / 3));

            if (GroundDirection.y > 0) return; 
            
            Vector2 position = transform.position;
            position.y = point.y + SizeY / 2;
            transform.position = position;
            
            gravityScale = Vector2.zero;
            IsGrounded = true;
            IsAerialPlatform = true;
        }
    }
     
    protected override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
        
        //공중 발판과 충돌 시
        if (other.gameObject.CompareTag(StringNameSpace.Tags.AerialPlatform))
        {
            Vector3 point = other.ClosestPoint(transform.position);
            GroundDirection = point - (transform.position - new Vector3(0,SizeY / 3));
            
            if (GroundDirection.y > 0) return; 
            
            Vector2 position = transform.position;
            position.y = point.y + SizeY / 2;
            transform.position = position;
        }
        
    }
    
    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        
        if (other.gameObject.CompareTag(StringNameSpace.Tags.AerialPlatform))
        {
            IsGrounded = false;
            IsAerialPlatform = false;
        }
    }

}
