using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public Attack heroAttack;
 
    public void PlayerAttack()
    {
        heroAttack.DoAttack();
    }
    public void PlayerDamage()
    {
        transform.GetComponentInChildren<EnemyAttack>().DamagePlayer();
        //transform.GetComponentInParent<EnemyController>().DamagePlayer();
    }
}
