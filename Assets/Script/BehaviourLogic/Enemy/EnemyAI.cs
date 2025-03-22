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

    public AudioClip walkAudioClip;    // Suara langkah
    public AudioClip chaseAudioClip;   // Suara ketika mengejar

    public AudioSource footstepSource; // Drag dari Inspector
    public AudioSource chaseSource;    // Drag dari Inspector

    private NavMeshAgent navAgent;
    private RandomMovement patrolScript;
    private bool isChasing = false;
    private bool isAggressive = false;
    private bool isPlayerHidden = false;
    private bool isStunned = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector3 lastPosition;
    private Coroutine stunCoroutine;


    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        patrolScript = GetComponent<RandomMovement>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        navAgent.updateRotation = true;
        lastPosition = transform.position;

        // Warning kalau lupa assign AudioSource
        if (footstepSource == null || chaseSource == null)
        {
            Debug.LogWarning("AudioSource belum di-assign di Inspector untuk EnemyAI!");
        }
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
                StopChaseAudio();
            }
            else if (distanceToPlayer > attackRange)
            {
               
                navAgent.speed = isAggressive ? aggressiveSpeed : chaseSpeed;
                navAgent.SetDestination(playerTransform.position);
                AdjustChaseAudio(distanceToPlayer);
            }
            else
            {
             
                AttackPlayer();
            }
        }
        else if ((distanceToPlayer <= detectionRange || isAggressive) && !isPlayerHidden)
        {
            isChasing = true;
            PlayChaseAudio();
        }

        HandleFootstepSound();
    }

    void AttackPlayer()
    {
        Debug.Log("Enemy menyerang Player!");

        GameManager gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager != null)
        {
            gameManager.GameOver();
        }

        StopChaseAudio();
    }

    

    private void PlayChaseAudio()
    {
        if (!chaseSource.isPlaying && chaseAudioClip != null)
        {
            chaseSource.clip = chaseAudioClip;
            chaseSource.loop = true;
            chaseSource.Play();
        }
    }

    private void StopChaseAudio()
    {
        if (chaseSource.isPlaying)
        {
            chaseSource.Stop();
        }
    }

    private void AdjustChaseAudio(float distance)
    {
        if (chaseSource.isPlaying && chaseAudioClip != null)
        {
            float minVolume = 0.3f;
            float maxVolume = 1.0f;
            chaseSource.volume = Mathf.Lerp(maxVolume, minVolume, distance / detectionRange);

            float minPitch = 1.0f;
            float maxPitch = 1.8f;
            chaseSource.pitch = Mathf.Lerp(maxPitch, minPitch, distance / detectionRange);
        }
    }
    private void HandleFootstepSound()
    {
        if ( !isStunned && navAgent.velocity.magnitude > 0.1f)
        {
            if (!footstepSource.isPlaying)
            {
                footstepSource.clip = walkAudioClip;
                footstepSource.loop = true;
                footstepSource.Play();
            }
        }
        else
        {
            if (footstepSource.isPlaying)
            {
                footstepSource.Stop();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
            PlayChaseAudio();
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
    public void StunEnemy(float stunDuration)
    {
        if (!isStunned)
        {
            isStunned = true;
            navAgent.isStopped = true;

            if (animator != null)
            {
                animator.SetBool("isStunned", true);
            }

            if (stunCoroutine != null)
            {
                StopCoroutine(stunCoroutine);
            }

            stunCoroutine = StartCoroutine(StunRoutine(stunDuration));
        }
    }

    IEnumerator StunRoutine(float duration)
    {
        isStunned = true;
        navAgent.isStopped = true;
        navAgent.velocity = Vector3.zero;

        Debug.Log("Musuh terkena stun selama " + duration + " detik!");

        StopChaseAudio();
        footstepSource.Stop();

        yield return new WaitForSeconds(duration);

        isStunned = false;
        navAgent.isStopped = false;

        if (animator != null)
        {
            animator.SetBool("isStunned", false); // reset animasi stun
        }

        Debug.Log("Stun selesai, musuh kembali mengejar!");
    }



    public void SetPlayerHidden(bool hidden)
    {
        isPlayerHidden = hidden;
        if (hidden)
        {
            isChasing = false;
            isAggressive = false;
            Debug.Log("Player bersembunyi di rumput, Enemy tidak bisa melihat.");
            StopChaseAudio();
        }
        else
        {
            Debug.Log("Player keluar dari persembunyian.");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isAggressive ? Color.yellow : Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (navAgent != null && navAgent.hasPath)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, navAgent.destination);
        }
    }
}
