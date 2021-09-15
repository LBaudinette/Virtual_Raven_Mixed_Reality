using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    bool fired;
    public float fireForce;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        fired = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void OnFired()
    {
        fired = true;
        Vector3 fireAngle = Vector3.forward;
        rb.AddForce(fireAngle * fireForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
