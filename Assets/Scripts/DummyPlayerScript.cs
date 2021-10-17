using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayerScript : MonoBehaviour
{
    public static Transform playerPosition;

    [SerializeField]
    private float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(int damage) {
        health -= damage;
    }
}
