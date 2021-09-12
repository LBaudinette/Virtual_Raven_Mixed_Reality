using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // maybe static?
    public int startingScore = 0;
    public int startingGateHealth = 1000;

    public static int gatehealth;
    public static int score;

    private void Start()
    {
        ResetPlayerStats();
    }

    public void ResetPlayerStats()
    {
        gatehealth = startingGateHealth;
        score = startingScore;
    }
}
