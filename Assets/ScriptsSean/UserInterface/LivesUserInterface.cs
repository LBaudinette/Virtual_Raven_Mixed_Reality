using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUserInterface : MonoBehaviour
{
    public Image gateHealth;
    [SerializeField] private int startingGateHealth = 100;

    // Update is called once per frame
    void Update()
    {
        gateHealth.fillAmount = (float)PlayerStats.gatehealth / (float)startingGateHealth;
        //Debug.Log("gate health" + PlayerStats.gatehealth.ToString());
        //Debug.Log("fill amount" + gateHealth.fillAmount.ToString());
    }
}
