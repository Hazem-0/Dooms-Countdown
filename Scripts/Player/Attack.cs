using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Animator animator;
    CapsuleCollider wepon;

    //EnemyStatus enemyStatus;
    Transform root;


    //float lastAttack = 0f;
    readonly float weponColliderCoolDown = 0.15f;
    //bool isAttacking = false;
    void Start()
    {
        //animator = GetComponentInChildren<Animator>();
        wepon = GetComponent<CapsuleCollider>();
        GetMainParent();
        animator = root.GetComponentInChildren<Animator>();

    }
    void GetMainParent()
    {
        root = transform;
        while (root.parent != null)
        {
            root = root.parent;
        }
    }
    void Update()
    {
        HeroAttack();
    }

    // apply single attack 
    void HeroAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z))   
            MakeAttack();
        
    }
    // TODO : Special Attack






    private void MakeAttack()
    {
        animator.SetTrigger("Attack");
       Instantiate(LevelManager.Instance.Particles[0], transform.position, transform.rotation);

        StartCoroutine(AttackCooldown());
    }

    // Add combo & if need to change to continue press key
    IEnumerator AttackCooldown()
    {
        //isAttacking = true;

        // Wait for attack animation to finish (adjust as needed for attack length)
        yield return new WaitForSeconds(1f);
        //isAttacking = false;
    }

    public void DoAttack()
    {
        wepon.enabled = true;
        StartCoroutine(HideCollider());

    }
    IEnumerator HideCollider()
    {
        yield return new WaitForSeconds(weponColliderCoolDown);
        wepon.enabled = false;
    }
   
    
}
