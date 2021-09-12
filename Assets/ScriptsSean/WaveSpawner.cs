using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform[] spawnPoints;
    //public Transform endPoint;

    public float timeBetweenWaves = 10.5f;
    private float countdown = 2f;

    public Text waveCountDownText;

    private int waveIndex = 0;

    // Update is called once per frame
    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            SpawnWave();
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        //waveCountDownText.text = Mathf.CeilToInt(countdown).ToString();
        waveCountDownText.text = string.Format("{0:00.00}", countdown);
    }

    private IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < Mathf.FloorToInt(Mathf.Sqrt(2* waveIndex)); i++)
        {
            spawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void spawnEnemy()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(enemyPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }
}
