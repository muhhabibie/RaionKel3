using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform centrePoint; // Titik pusat area gerak
    public float range = 20; 

    public List<Collider> stages; // List Collider untuk setiap stage
    private int currentStageIndex = 0; // Stage aktif

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetStage(currentStageIndex); // Atur stage awal

        agent.isStopped = false;
        agent.updatePosition = true;
        agent.updateRotation = true;

        MoveToRandomPoint(); 
    }

    void Update()
    {
        if (!agent.enabled)
            return;

        if (agent.remainingDistance <= agent.stoppingDistance)
            MoveToRandomPoint();
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

    // Pindah ke stage tertentu
    public void SetStage(int stageIndex)
    {
        if (stageIndex >= 0 && stageIndex < stages.Count)
        {
            currentStageIndex = stageIndex;
            centrePoint = stages[stageIndex].transform;
            range = stages[stageIndex].bounds.size.x / 2; // Sesuaikan range dengan ukuran stage
        }
    }

    void OnTriggerEnter(Collider other)
    {
        int newStageIndex = stages.IndexOf(other);
        if (newStageIndex != -1 && newStageIndex != currentStageIndex)
        {
            SetStage(newStageIndex);
        }
    }
}
