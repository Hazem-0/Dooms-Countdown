using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    CapsuleCollider wepon;
    Transform enemy;
    EnemyStatus enemyStatus;
    //EnemyStatus enemyStatus;

    PlayerStatus playerStatus;
    Animator playerAnimator;


    //float lastAttack = 0f;
    readonly float weponColliderCoolDown = 0.1f;
    //bool isAttacking = false;
    void Start()
    {
        //animator = GetComponentInChildren<Animator>();
        wepon = GetComponent<CapsuleCollider>();
        GetMainParent();
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        playerStatus = LevelManager.Instance.Player.GetComponent<PlayerStatus>();
        playerAnimator = LevelManager.Instance.Player.GetComponentInChildren<Animator>();
    }
    void GetMainParent()
    {
        Debug.Log("I am here to find parent");
        enemy = transform;
        while (enemy.parent != null)
        {
            enemy = enemy.parent;
        }
    }
    public void DamagePlayer()
    {
        wepon.enabled = true;
        Debug.Log("Attack by Enemy");
        StartCoroutine(HideCollider());

    }
    IEnumerator HideCollider()
    {
        yield return new WaitForSeconds(weponColliderCoolDown);
        wepon.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("DamagePlayer");
            playerAnimator.SetTrigger("Hit");
            float reduceHealth = enemyStatus.Attack > playerStatus.defence ? enemyStatus.Attack - playerStatus.defence : 1;
            playerStatus.ChangeHealth(-reduceHealth);

            if (playerStatus.CurrentHealth <= 0)
            {
                playerAnimator.SetTrigger("Die");
                // TODO:Stop game


            }
        }
    }

}
