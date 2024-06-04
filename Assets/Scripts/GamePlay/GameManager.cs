using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Enemy Spawn Related")]
    [SerializeField] private Player Player;
    [SerializeField] private PlayerStatus PlayerStatus;
    [SerializeField] private int currentLvl;
    public Enemyspawner spawnManager;
    public int currentWaves =0;
    public int currentWaveEnemyCount;
    public int spawnAmount;
    public int rareSpawnAmount;
    public int spawnIncrement;
    public int totalWaves;

    [SerializeField] private int coins;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentLvl = Player.getLvl();
        PlayerStatus.UpdateLvlText(currentLvl);
    }

    private void FixedUpdate()
    {
        currentLvl = Player.getLvl();
        PlayerStatus.UpdateLvlText(currentLvl);
        Debug.Log("lvl updated");
    }

    public void SetUpWave()
    {
        currentWaveEnemyCount = spawnManager.SpawnWave(spawnAmount, rareSpawnAmount);
        spawnAmount += spawnIncrement;
        rareSpawnAmount += 5;
        currentWaves += 1;
    }

    public void addKill()
    {
        currentWaveEnemyCount--;
        if (currentWaveEnemyCount == 0)
        {
            if (currentWaves == totalWaves)
            {
                spawnManager.SpawnBoss();
            }
            else
            {
                currentWaveEnemyCount = spawnManager.SpawnWave(spawnAmount, rareSpawnAmount);
                currentWaves++;
                spawnAmount += 5;

            }
        }
    }

    public void addCoin(int _coinAmount)
    {
        coins += _coinAmount;
    }

}
