using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    //필드
    private BoxCollider2D _boxCollider;
    private Enemy _enemy;
    
    //직렬화
    [SerializeField] private GameObject monster;
    
    //프로퍼티
    public Transform SpawnPoint {get => transform;}

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        //Test Code
        Spawn();
    }
    
    public void Spawn()
    {
        GameObject go =Instantiate(monster, SpawnPoint.position, SpawnPoint.rotation);
        _enemy = go.GetComponent<Enemy>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            _enemy.Target = other.gameObject.transform;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            _enemy.Target = null;
        }
    }
}
