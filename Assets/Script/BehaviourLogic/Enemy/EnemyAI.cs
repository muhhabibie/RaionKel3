using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;
    public float detectionRange;
    public float attackRange;
    public float chaseSpeed;
    public float aggressiveSpeed;

    private NavMeshAgent navAgent;
    private RandomMovement patrolScript;
    private bool isChasing = false;
    private bool isAggressive = false;
    private bool isPlayerHidden = false;
    private bool isStunned = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        patrolScript = GetComponent<RandomMovement>();
      

        Debug.Log("EnemyAI dimulai!");
    }

    void Update()
    {
        if (!navAgent.enabled || isPlayerHidden || isStunned) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (isChasing)
        {
            if (distanceToPlayer > detectionRange && !isAggressive)
            {
                isChasing = false;
                patrolScript.MoveToRandomPoint();
                Debug.Log("Musuh kehilangan jejak, kembali berpatroli.");
            }
            else if (distanceToPlayer > attackRange)
            {
        
                navAgent.speed = isAggressive ? aggressiveSpeed : chaseSpeed;
                navAgent.SetDestination(playerTransform.position);
              

                FaceTarget();
            }
            else
            {
                AttackPlayer();
            }
        }
        else if ((distanceToPlayer <= detectionRange || isAggressive) && !isPlayerHidden)
        {
            isChasing = true;
            Debug.Log("Enemy mulai mengejar Player!");
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Enemy menyerang Player!");
        Destroy(playerTransform.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
        }
        else if (other.CompareTag("LightZone"))
        {
            isAggressive = true;
            navAgent.speed = aggressiveSpeed;
            Debug.Log("Enemy masuk LightZone, menjadi agresif!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightZone") && !isAggressive) 
        {
            isAggressive = false;
            navAgent.speed = chaseSpeed;
            Debug.Log("Enemy keluar dari LightZone, kembali normal.");
        }
    }

    public void StunEnemy(float duration)
    {
        if (!isStunned)
        {
            StartCoroutine(StunRoutine(duration));
        }
    }

    IEnumerator StunRoutine(float duration)
    {
        isStunned = true;
        navAgent.isStopped = true;
        Debug.Log("Musuh terkena stun selama " + duration + " detik!");

        yield return new WaitForSeconds(duration);

        isStunned = false;
        navAgent.isStopped = false;
        Debug.Log("Stun selesai, musuh kembali mengejar!");
    }

    public void ForceChasePlayer()
    {
        if (!isStunned)
        {
            isChasing = true;
            isAggressive = true; 
            navAgent.isStopped = false;
            navAgent.speed = aggressiveSpeed;
            navAgent.SetDestination(playerTransform.position);

            Debug.Log("Musuh dipaksa mengejar Player oleh jaring laba-laba!");
        }
    }

    public void SetPlayerHidden(bool hidden)
    {
        isPlayerHidden = hidden;
        if (hidden)
        {
            isChasing = false; 
            isAggressive = false; 
            Debug.Log("Player bersembunyi di rumput, Enemy tidak bisa melihat.");
        }
        else
        {
            Debug.Log("Player keluar dari persembunyian.");
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
    private void OnDrawGizmos()
    {
        if (isAggressive)
        {
            Gizmos.color = Color.yellow; //Area agresif ketika Enemy masuk LightZone
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
        else
        {
            Gizmos.color = Color.green; 
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }

        Gizmos.color = Color.red; // Jarak serangan musuh
        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (isStunned)
        {
            Gizmos.color = Color.blue; 
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}

