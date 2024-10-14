using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics; // For Stopwatch

public class Flock : MonoBehaviour
{
    public Drone agentPrefab;
    List<Drone> agents = new List<Drone>();
    public FlockBehavior behavior;

    [Range(10, 5000)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private Stopwatch updateStopwatch;
    private Stopwatch partitionStopwatch;

    // Start is called before the first frame update
    void Start()
    {
        updateStopwatch = new Stopwatch();
        partitionStopwatch = new Stopwatch();

        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            Drone newAgent = Instantiate(
                agentPrefab,
                UnityEngine.Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    // O(N) partition function based on Ammo attribute with static camo colors
    void PartitionDronesByAmmo()
    {
        if (agents.Count == 0) return;

        partitionStopwatch.Start(); // Start timing the partition function

        // Use the first drone's Ammo as the pivot value
        int pivotAmmo = agents[0].Ammo;

        foreach (Drone agent in agents)
        {
            if (agent.Ammo <= pivotAmmo)
            {
                // Lower Ammo - set to a military green color (dark green)
                agent.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.3f, 0.1f); // Dark Green
            }
            else
            {
                // Higher Ammo - set to a tan/brown color
                agent.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.5f, 0.4f); // Tan/Brown
            }
        }

        partitionStopwatch.Stop(); // Stop timing the partition function
        UnityEngine.Debug.Log("PartitionDronesByAmmo execution time: " + partitionStopwatch.Elapsed.TotalMilliseconds.ToString("F3") + " ms");
        partitionStopwatch.Reset(); // Reset stopwatch for the next call
    }

    // Update is called once per frame
    void Update()
    {
        updateStopwatch.Start(); // Start timing the Update function

        // Call the partition function here
        PartitionDronesByAmmo();

        foreach (Drone agent in agents)
        {
            // decide on next movement direction
            List<Transform> context = GetNearbyObjects(agent);

            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }

        updateStopwatch.Stop(); // Stop timing the Update function
        UnityEngine.Debug.Log("Update execution time: " + updateStopwatch.Elapsed.TotalMilliseconds.ToString("F3") + " ms");
        updateStopwatch.Reset(); // Reset stopwatch for the next call
    }

    List<Transform> GetNearbyObjects(Drone agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
