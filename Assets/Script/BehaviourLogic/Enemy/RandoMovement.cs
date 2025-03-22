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
      
   

        lastPosition = transform.position;

        MoveToRandomPoint();
    }

    void Update()
    {
        if (!agent.enabled) return;

        if (agent.remainingDistance <= agent.stoppingDistance)
            MoveToRandomPoint();

        // Deteksi arah gerakan untuk animasi
        Vector3 movementDirection = transform.position - lastPosition;
        lastPosition = transform.position;

        float horizontalSpeed = movementDirection.x;
        float verticalSpeed = movementDirection.z;

        animator.SetFloat("Speed", agent.velocity.magnitude);

        // Reset semua arah dulu
        animator.SetBool("JalanKanan", false);
        animator.SetBool("JalanKiri", false);

        if (Mathf.Abs(horizontalSpeed) > Mathf.Abs(verticalSpeed))
        {
            if (horizontalSpeed > 0.01f)
            {
          
                animator.SetBool("JalanKanan", true);
                spriteRenderer.flipX = false;

            }
            else if (horizontalSpeed < -0.01f)
            {
                animator.SetBool("JalanKiri", true);
                spriteRenderer.flipX = false;
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