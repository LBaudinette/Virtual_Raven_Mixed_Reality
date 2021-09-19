using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : MonoBehaviour
{
    public LayerMask enemyLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy")) {
            Debug.Log("COLLIDE");
            //Play effect
            //Get all enemies within a radius
            Collider[] nearbyCollisions =
                Physics.OverlapSphere(collision.GetContact(0).point, 50f, enemyLayer);

            foreach(Collider collider in nearbyCollisions) {
                Debug.Log("COLLISION NAME " + collider.gameObject.name);
                //TODO: Have damage over time method
                collider.GetComponent<Enemy>().TakeDamage(50);
            }
            Destroy(gameObject);
        }
        
    }
    
}
