using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUserInterface : MonoBehaviour
{
    public Text livesText;

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Gate\n" + PlayerStats.gatehealth.ToString();
    }
}
