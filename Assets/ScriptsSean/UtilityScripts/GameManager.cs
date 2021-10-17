using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool gameIsOver;

    public GameObject gameOverUI;
    public ParticleSystem smokeParticles;

    private void Start()
    {
        gameIsOver = false;
        smokeParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
        {
            return;
        }

        if (PlayerStats.gatehealth <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameIsOver = true;
        smokeParticles.Play();
        gameOverUI.SetActive(true);
    }
}
