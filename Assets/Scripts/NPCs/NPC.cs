using UnityEngine;
using UnityEngine.AI;

interface I_NPC
{
    public string Name { get; }
    public uint Id { get; }
    public Transform[] Waypoints { get; }
    public int currentWaypointIndex { get; set;  }
    public float Speed { get; set; }
    public NavMeshAgent NavMeshAgent { get; }
}


public class NPC : MonoBehaviour, I_NPC
{
    public string Name { get; set; }
    public uint Id { get; set; }
    public Transform[] Waypoints { get; set; }
    public int currentWaypointIndex { get; set; }
    public float Speed { get; set; }
    public NavMeshAgent NavMeshAgent { get; private set; }


    public void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.destination = Waypoints[currentWaypointIndex].position;
    }

    public void Update()
    {
        if (!NavMeshAgent.pathPending && NavMeshAgent.remainingDistance < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % Waypoints.Length;
            NavMeshAgent.destination = Waypoints[currentWaypointIndex].position;
        }
    }

}
