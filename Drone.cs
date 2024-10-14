using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Drone : MonoBehaviour
{
    public int Ammo { set; get; } // Ammo attribute
    public int WeaponCapacity { set; get; } // Weapon Capacity attribute
    public int Temperature { set; get; } = 0;

    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    private void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        RandomizeAttributes(); // Initialize with random values
        StartCoroutine(RandomizeAttributesPeriodically()); // Randomize at regular intervals
    }

    private void Update()
    {
        Temperature = (int)(Random.value * 100);
    }

    // Coroutine to randomize attributes periodically
    private IEnumerator RandomizeAttributesPeriodically()
    {
        while (true)
        {
            RandomizeAttributes();
            yield return new WaitForSeconds(2f); // Adjust the interval as needed
        }
    }

    private void RandomizeAttributes()
    {
        Ammo = Random.Range(1, 100); // Random Ammo between 1 and 100
        WeaponCapacity = Random.Range(1, 100); // Random Weapon Capacity between 1 and 100
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
