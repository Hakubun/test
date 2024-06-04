using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject itemContainer;
    [SerializeField] private GameObject[] spawnableItems;
    [SerializeField] private GameObject[] rareItems;

    [Header("Spawn Settings")]
    private bool spawning;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float minWaitTime = 5f;
    [SerializeField] private float maxWaitTime = 10f;
    [SerializeField] private int maxItemCount = 10;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            FindPlayer();
        }

        spawning = true;
        StartCoroutine(SpawnRoutine());

    }

    void FindPlayer()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player reference not setted");
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (spawning)
        {
            SpawnItem();
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnItem()
    {
        Vector3 posToSpawn = SpawnLocation(spawnRadius);
        GameObject itemToSpawn;
        if (Random.value < 0.2f && rareItems.Length > 0)
        {
            itemToSpawn = rareItems[Random.Range(0, rareItems.Length)];
        }
        else
        {
            itemToSpawn = spawnableItems[Random.Range(0, spawnableItems.Length)];
        }

        GameObject item = Instantiate(itemToSpawn, posToSpawn + new Vector3 (0f, 0.5f, 0f), Quaternion.identity);
        item.transform.parent = itemContainer.transform;
    }

    private Vector3 SpawnLocation(float radius)
    {
        Vector3 randomDirection = Random.onUnitSphere;

        randomDirection *= radius;

        Vector3 spawnPosition = player.transform.position + randomDirection;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPosition, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return spawnPosition;
        }
    }

}
