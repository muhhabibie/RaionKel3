using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform centrePoint;
    public float range = 20;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector3 lastPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        agent.isStopped = false;
        agent.updatePosition = true;
        agent.updateRotation = true;

        lastPosition = transform.position;

        MoveToRandomPoint();
    }

    void Update()
    {
        if (!agent.enabled) return;

        if (agent.remainingDistance <= agent.stoppingDistance)
            MoveToRandomPoint();

        // DETEKSI ARAH GERAKAN UNTUK ANIMASI
        Vector3 movementDirection = transform.position - lastPosition;
        lastPosition = transform.position;

        float horizontalSpeed = movementDirection.x; // Gerakan kiri/kanan
        float verticalSpeed = movementDirection.z;  // Gerakan maju/mundur

        if (Mathf.Abs(horizontalSpeed) > Mathf.Abs(verticalSpeed))
        {
            // Lebih dominan bergerak ke kiri/kanan
            animator.SetFloat("Speed", Mathf.Abs(horizontalSpeed));
            spriteRenderer.flipX = horizontalSpeed < 0;
        }
        else
        {
            // Lebih dominan bergerak ke depan/belakang
            animator.SetFloat("Speed", Mathf.Abs(verticalSpeed));

            // Rotasi agar sprite menghadap ke arah gerakan
            if (verticalSpeed > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0); // Hadap depan
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0); // Hadap belakang
            }
        }
    }

    public void MoveToRandomPoint()
    {
        Vector3 point;
        if (RandomPoint(centrePoint.position, range, out point))
        {
            agent.SetDestination(point);
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }
}
