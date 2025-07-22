using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushWeapon : EnemyWeapon
{
    public EnemyRushAttack RushEnemy { get; set; }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);


        RushEnemy.RushKnuckBack(other.gameObject.transform, knockBackForce);
        
    }
}
