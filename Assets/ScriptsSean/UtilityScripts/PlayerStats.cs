using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // maybe static?
    public int startingScore = 0;
    public int startingGateHealth = 100;
    public int StartingPlayerHealth = 100;

    public static int gatehealth;
    public static int score;
    public static int playerHealth;

    private void Start()
    {
        ResetPlayerStats();
    }

    public void ResetPlayerStats()
    {
        gatehealth = startingGateHealth;
        score = startingScore;
        playerHealth = StartingPlayerHealth;
    }

    public static void AddToScore(int additionalScore)
    {
        score += additionalScore;
    }

    public static void MinusPlayerHealth(int minusHealthAmount)
    {
        playerHealth -= minusHealthAmount;
    }

    public static void MinusGateHealth(int minusHealthAmount)
    {
        gatehealth -= minusHealthAmount;
    }
}
