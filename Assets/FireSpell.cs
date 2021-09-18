using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            //Debug.Log("COLLIDE");
            //Play effect
            //Get all enemies within a radius
            //TODO: add enemy layers
            Collider[] nearbyCollisions =
                Physics.OverlapSphere(gameObject.transform.position, 10f);

            foreach(Collider collider in nearbyCollisions) {
                //Get the script of the enemy and apply the coroutine
            }
            Destroy(gameObject);
        }
    }
}
