using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemyspawner : MonoBehaviour
{
    [SerializeField] private GameObject[] MeleePrefabs;
    [SerializeField] private GameObject[] RangePrefabs;
    [SerializeField] private GameObject BossPrefab;
    [SerializeField] private GameObject[] BossPrefabsChallenge;
    [SerializeField] public GameObject enemyContainer;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnRadius = 60f;
    [SerializeField] private int maxEnemies = 20;
    [SerializeField] private int meleeToRangeRatio = 5;

    private Transform enemyContainerTransform;
    private NavMeshHit hit;
    private int currentEnemyCount;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemyContainerTransform = enemyContainer.transform;
    }


    public void SpawnEnemy(GameObject[] enemyPrefabArray)
    {
        Vector3 spawnPosition = randomNavMeshPoint();
        if (spawnPosition != Vector3.zero)
        {
            GameObject enemyPrefab = enemyPrefabArray[Random.Range(0, enemyPrefabArray.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.transform.parent = enemyContainerTransform;

            currentEnemyCount++;
        }
    }

    public void SpawnBoss()
    {
        Vector3 spawnPosition = randomNavMeshPoint();
        if (spawnPosition != Vector3.zero)
        {
            Instantiate(BossPrefab, spawnPosition, Quaternion.identity);

        }
    }

    public int SpawnWave(int amount, int rareEnemyAmount)
    {
        currentEnemyCount = 0;
        for (int i = 0; i < amount; i++)
        {
            SpawnEnemy(MeleePrefabs);
        }
        for (int j = 0; j < rareEnemyAmount; j++)
        {
            SpawnEnemy(RangePrefabs);
        }
        return currentEnemyCount;
    }

    private Vector3 randomNavMeshPoint()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * spawnRadius;
        randomDirection += player.position;

        if (NavMesh.SamplePosition(randomDirection, out hit, spawnRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return Vector3.zero;

    }
}
