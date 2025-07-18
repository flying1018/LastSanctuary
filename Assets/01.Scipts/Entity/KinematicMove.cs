using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicMove : MonoBehaviour
{
    //필드
    private Rigidbody2D _rigidbody;
    
    public Vector2 gravityScale = Vector2.zero;
    
    //프로퍼티
    public Vector2 GroundDirection { get; set; }
    public Vector2 WallDirection { get; set; }
    public bool IsWall { get; set; }
    public bool IsGrounded { get; set; }
    public bool IsAerialPlatform { get; set; }
    public float SizeX { get; set; }
    public float SizeY { get; set; }

    
    public void Init(float sizeX, float sizeY , Rigidbody2D rigidbody)
    {
        SizeX = sizeX;
        SizeY = sizeY;
        _rigidbody = rigidbody;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //바닥 충돌 시
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Ground))
        {
            Vector3 point = other.ClosestPoint(transform.position);
            GroundDirection = point - transform.position;
            
            Vector2 position = transform.position;
            position.y = point.y + SizeY / 2;
            transform.position = position;
            
            gravityScale = Vector2.zero;
            IsGrounded = true;
        }

        //천장과 충돌 시
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Celling))
        {
            Vector3 point = other.ClosestPoint(transform.position);
            GroundDirection = point - transform.position;
            
            Vector2 position = transform.position;
            position.y = point.y - SizeY / 2;
            transform.position = position;
        }
        
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
        
        
        
        //벽과 충돌 시
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Wall))
        {
            Vector3 point = other.ClosestPoint(transform.position);
            WallDirection = point - transform.position;

            Vector2 position = transform.position;
            position.x = point.x + (WallDirection.x < 0 ? SizeX : -SizeX/2);
            transform.position = position;
            
            IsWall = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //땅과 충돌 시
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Ground))
        {
            Vector3 point = other.ClosestPoint(transform.position);
            GroundDirection = point - transform.position;
            
            Vector2 position = transform.position;
            position.y = point.y + (GroundDirection.y < 0 ? SizeY / 2 : -SizeY/2);
            transform.position = position;
        }
        
        
        //벽과 충돌 시
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Wall))
        {
            Vector3 point = other.ClosestPoint(transform.position);
            WallDirection = point - transform.position;

            Vector2 position = transform.position;
            position.x = point.x + (WallDirection.x < 0 ? SizeX / 2 : -SizeX/2);
            transform.position = position;
        }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Ground))
        {
            IsGrounded = false;
        }
        
        if (other.gameObject.CompareTag(StringNameSpace.Tags.AerialPlatform))
        {
            IsGrounded = false;
            IsAerialPlatform = false;
        }
        
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Wall))
        {
            IsWall = false;
        }
    }
    
    //캐릭터의 이동 처리
    public void Move(Vector2 direction)
    {
        Vector2 horizontal = direction;
        Vector2 vertical = direction;
        
        horizontal.y = 0;
        vertical.x = 0;
        
        if (IsWall && WallDirection.normalized == horizontal.normalized) direction.x = 0;
        if (IsGrounded && GroundDirection.normalized == vertical.normalized) direction.y = 0;
        
        Debug.Log(direction);
        
        _rigidbody.MovePosition(_rigidbody.position + direction * Time.fixedDeltaTime);
    }

    public Vector2 Horizontal(Vector2 direction, float speed)
    {
        direction.y = 0;
        return direction.normalized * speed;
    }

    public Vector2 Vertical(Vector2 direction, float speed)
    {
        direction.x = 0;
        return direction.normalized * speed;
    }

    public Coroutine addForceCoroutine;
    public void AddForce(Vector2 force,float dumping = 0.95f)
    {
        if (addForceCoroutine != null)
        {
            StopCoroutine(addForceCoroutine);
            addForceCoroutine = null;
        }
        
        addForceCoroutine = StartCoroutine(AddForce_Coroutine(force,dumping));;
    }

    IEnumerator AddForce_Coroutine(Vector2 force, float dumping)
    {
        while (force.magnitude > 0.01f)
        {
            Move(force);
            yield return null;
            force *= dumping;
        }
        
        addForceCoroutine = null;
    }
}
