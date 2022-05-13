using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIControl : MonoBehaviour
{
    [Header("Enemy AI Control Attributes")]
    Transform player;
    [SerializeField] Transform[] walkPoints;
    [SerializeField] int walkIndex = 0;
    Animator animator;
    NavMeshAgent naveMeshAgent;
    public float walkDistance = 8.0f;
    public float attackDistance = 2.5f;
    float currentAttackTime = 0.0f;
    float waitAttackTime = 0.50f;
    Vector3 nextDestination = Vector3.zero;
    EnemyHealth enemyHealth;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        naveMeshAgent = GetComponent<NavMeshAgent>();

        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth.health > 0)
        {
            MoveAndAttack();
        }
        else
        {
            animator.SetBool("Death",true);
            naveMeshAgent.enabled = false;

            if(!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                Destroy(gameObject,2.0f);
            }
        }
    }

    void MoveAndAttack()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance > walkDistance)
        {
            if(naveMeshAgent.remainingDistance <= 0.5f)
            {
                naveMeshAgent.isStopped = false;

                animator.SetBool("Walk",true);
                animator.SetBool("Run", false);
                animator.SetInteger("Atk", 0);

                nextDestination = walkPoints[walkIndex].position;
                naveMeshAgent.SetDestination(nextDestination);

                if(walkIndex == walkPoints.Length - 1)
                {
                    walkIndex = 0;
                }
                else
                {
                    walkIndex++;
                }
            }
        }
        else
        {
            if(distance > attackDistance)
            {
                naveMeshAgent.isStopped = false;

                animator.SetBool("Walk",false);
                animator.SetBool("Run",true);
                animator.SetInteger("Atk",0);

                naveMeshAgent.SetDestination(player.position);
            }
            else
            {
                naveMeshAgent.isStopped = true;
             
                animator.SetBool("Run", false);

                Vector3 targetPosition = new Vector3(player.position.x,transform.position.y,player.position.z);

                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(targetPosition - transform.position),5f * Time.deltaTime);

                if(currentAttackTime >= waitAttackTime)
                {
                    int atkRange = Random.Range(1, 3);
                    animator.SetInteger("Atk",atkRange);
                    currentAttackTime = 0f;
                }
                else
                {
                    animator.SetInteger("Atk",0);
                    currentAttackTime += Time.deltaTime;
                }
                
            }
        }
    }
}
