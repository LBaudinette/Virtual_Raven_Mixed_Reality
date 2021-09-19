using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreInterface : MonoBehaviour
{
    [SerializeField] private Text scoreCountText;
    [SerializeField] private Image playerHealth;
    [SerializeField] private int StartingPlayerHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUserInterface();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUserInterface();
    }

    private void UpdateUserInterface()
    {
        scoreCountText.text = PlayerStats.score.ToString();

        playerHealth.fillAmount = (float)PlayerStats.playerHealth / (float)StartingPlayerHealth;
    }
}
