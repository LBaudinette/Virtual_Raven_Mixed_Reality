using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    //scene componenent references
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    // time variables
    [SerializeField] private float timeBetweenWaves = 10.5f;
    private float countDownUntilWave = 10f;

    //public Text waveCountDownText;

    private int waveIndex = 0;

    // Update is called once per frame
    private void Update()
    {
        if (countDownUntilWave <= 0f)
        {
            StartCoroutine(SpawnWave());
            SpawnWave();
            countDownUntilWave = timeBetweenWaves;
        }

        countDownUntilWave -= Time.deltaTime;

        countDownUntilWave = Mathf.Clamp(countDownUntilWave, 0f, Mathf.Infinity);

        //waveCountDownText.text = Mathf.CeilToInt(countdown).ToString();
        //waveCountDownText.text = string.Format("{0:00.00}", countDownUntilWave);
    }

    private IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < Mathf.FloorToInt(Mathf.Sqrt(2* waveIndex)); i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(enemyPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }
}
