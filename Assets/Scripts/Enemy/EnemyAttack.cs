using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
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
    readonly float weponColliderCoolDown = 0.15f;
    //bool isAttacking = false;
    void Start()
    {
        //animator = GetComponentInChildren<Animator>();
        wepon = GetComponent<CapsuleCollider>();
        GetMainParent();
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        playerStatus = LevelManager.Instance.Player.GetComponent<PlayerStatus>();
        playerAnimator = LevelManager.Instance.Player.GetComponentInChildren<Animator>();
        wepon.enabled = false;
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

            float reduceHealth = enemyStatus.Attack > playerStatus.defence
                ? enemyStatus.Attack - playerStatus.defence
                : 1;

            playerStatus.ChangeHealth(-reduceHealth);

            if (playerStatus.CurrentHealth <= 0)
            {
                
                StartCoroutine(HandlePlayerDeath());
            }
        }
    }

     
    

    private IEnumerator HandlePlayerDeath()
    {
        playerAnimator.SetTrigger("Die");

        yield return new  WaitForNextFrameUnit();
        
        LevelManager.Instance.MainCanvas.Find("Game-over").gameObject.SetActive(true);
        //LevelManager.Instance.MainCanvas.Find("Game-over")..SetActive(true);

        Time.timeScale = 0f;
    }

}
