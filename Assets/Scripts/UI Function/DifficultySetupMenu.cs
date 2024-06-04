using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySetupMenu : MonoBehaviour
{
    int InitialSpawnAmount = 10;
    int EnemyIncrement = 5;
    int EasyTotalWave = 5;

    int NormalInitialSpawnAmount = 20;
    int NormalEnemyIncrement = 10;
    int NormalTotalWave = 10;

    int DifficultInitialSpawnAmount = 20;
    int DifficultEnemyIncrement = 10;
    int DifficultTotalWave = 10;
    public void EasyMode()
    {
        GameManager.Instance.spawnAmount = InitialSpawnAmount;
        GameManager.Instance.spawnIncrement = EnemyIncrement;
        GameManager.Instance.totalWaves = EasyTotalWave;
        GameManager.Instance.SetUpWave();
        this.gameObject.SetActive(false);
    }

    public void NormalMode()
    {
        GameManager.Instance.spawnAmount = NormalInitialSpawnAmount;
        GameManager.Instance.spawnIncrement = NormalEnemyIncrement;
        GameManager.Instance.totalWaves = NormalTotalWave;
        GameManager.Instance.SetUpWave();
        this.gameObject.SetActive(false);
    }

    public void DifficultMode()
    {
        GameManager.Instance.spawnAmount = DifficultInitialSpawnAmount;
        GameManager.Instance.spawnIncrement = DifficultEnemyIncrement;
        GameManager.Instance.totalWaves = DifficultTotalWave;
        GameManager.Instance.SetUpWave();
        this.gameObject.SetActive(false);
    }



}
