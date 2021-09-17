using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSpawner : MonoBehaviour
{
    [SerializeField] private Wizard[] wizardArray;

    [SerializeField] private float timeBetweenSpawns = 5f;
    private float countDownUntilWave = 5f;

    // Update is called once per frame
    void Update()
    {
        if (countDownUntilWave <= 0f)
        {
            Debug.Log("Time to spawn wizard");
            SpawnEnemy(ChooseSpawnPoint());
            countDownUntilWave = timeBetweenSpawns;
        }

        countDownUntilWave -= Time.deltaTime;

        countDownUntilWave = Mathf.Clamp(countDownUntilWave, 0f, Mathf.Infinity);

        //waveCountDownText.text = Mathf.CeilToInt(countdown).ToString();
        //waveCountDownText.text = string.Format("{0:00.00}", countDownUntilWave);
    }

    private void SpawnEnemy(int spawnPointIndex)
    {
        if(spawnPointIndex < 0)
        {
            return;
        }

        wizardArray[spawnPointIndex].gameObject.SetActive(true);
        Debug.Log("spawn reached");
    }

    private int ChooseSpawnPoint()
    {
        int returnInt = -1;
        bool finishedDecidingSpawn = true;

        // check if there is a valid spawn
        for (int i = 0; i < wizardArray.Length; i++)
        {
            if(wizardArray[i].gameObject.activeSelf == false)
            {
                finishedDecidingSpawn = false;
            }
        }

        while (!finishedDecidingSpawn)
        {
            returnInt = Random.Range(0, wizardArray.Length);
            Debug.Log(wizardArray[returnInt]);
            if(!wizardArray[returnInt].gameObject.activeSelf)
            {
                finishedDecidingSpawn = true;
                Debug.Log("spawn found");
            } 
        }

        return returnInt;
    }
}
