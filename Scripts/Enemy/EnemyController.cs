using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyController : MonoBehaviour
{

    EnemyStatus enemyStatus;
    //PlayerStatus playerStatus;

    public Transform []movementPoint;
    int pointIndex = 0;

    public float attackRaduis = 10f;
    //[SerializeField] Transform Player;
    //public float runRaduis = 5f;


    bool canAttack = true;
    readonly float attackCoolDown = 1.5f; 
    readonly float stoppingDistance = 2f; 
    NavMeshAgent agent;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enemyStatus = GetComponent<EnemyStatus>();



    }
   
    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(transform.position, LevelManager.Instance.Player.position);
        EnemyMovement(distance);

    }

    private void EnemyMovement(float distance)
    {
        if (distance < attackRaduis)
        {
            agent.SetDestination(LevelManager.Instance.Player.position);
            RunAnimation();
            Attack(distance);
        }
        else
        {
            if (Vector3.Distance(transform.position, movementPoint[pointIndex].position) < 1f)
            {

                pointIndex++;
                pointIndex %= movementPoint.Length;

            }
            WalkAnimation();
            agent.SetDestination(movementPoint[pointIndex].position);
        }
    }
    //public void DamagePlayer()
    //{
    //    // TODO: Control distance between wepon of enemy and player
    //    Debug.Log("DamagePlayer");
    //    playerAnimator.SetTrigger("Hit");
    //    float reduceHealth = enemyStatus.Attack > playerStatus.defence ? enemyStatus.Attack - playerStatus.defence : 1;
    //    playerStatus.ChangeHealth(-reduceHealth);

    //    if (playerStatus.CurrentHealth <= 0)
    //    {
    //        playerAnimator.SetTrigger("Die");
    //        // TODO:Stop game


    //    }
    //}

    void Attack(float distance)
    {
        if (distance <= stoppingDistance)
        {
            if (canAttack)
            {

                StartCoroutine(CoolDown());
                // make animation 
                AttackAnimation();

            }
        }
    }


   
    IEnumerator CoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("PlayerWepon"))
        {
            HitAnimation();
            float reduceHealth = LevelManager.Instance.Player.GetComponent<PlayerStatus>().attack  > enemyStatus.Defence ? LevelManager.Instance.Player.GetComponent<PlayerStatus>().attack - enemyStatus.Defence:1;
            enemyStatus.ChangeHealth(-reduceHealth);
            if (enemyStatus.CurrentHealth <= 0)
            {
                DieAnimation();
                StartCoroutine(FallAfterDie());

            }
        }
    }
    #region Animation
    private void DieAnimation()
    {
        Debug.Log("Die Animation");
        animator.SetTrigger("Die");
    }
    void WalkAnimation()
    {
        animator.SetFloat("Speed", 0.5f);

    }
    void RunAnimation()
    {
        animator.SetFloat("Speed", 1f);
    }
    private void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
    private void HitAnimation()
    {
        animator.SetTrigger("Hit");
    }
    #endregion
    IEnumerator FallAfterDie()
    {
        agent.isStopped=true;
        yield return new WaitForSeconds(3f);
        enemyStatus.Die();

    }

    IEnumerator GameOver() {

        yield return new WaitForSeconds(3f);
         // Finish Game
    }
}
/*
 
 List of Animation needed:
 - Idel
 - walk
 - run

 - attack
 - hit
 - die
 -
 
 
 */