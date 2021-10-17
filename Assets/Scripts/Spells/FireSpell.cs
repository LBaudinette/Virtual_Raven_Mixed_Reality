using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireSpell : MonoBehaviour
{
    public LayerMask enemyLayer;

    public GameObject visualEffect;
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
            Debug.Log("COLLISION NAME " + collision.gameObject.name);

            //Play effect
            //Get all enemies within a radius
            Collider[] nearbyCollisions =
                Physics.OverlapSphere(collision.GetContact(0).point, 15f, enemyLayer);

            foreach(Collider collider in nearbyCollisions) {

                if (collider.gameObject.CompareTag("Enemy")) {
                    Enemy enemyScript = collider.gameObject.GetComponent<Enemy>();
                    enemyScript.TakeDamage(100);
                    enemyScript.Slow(50);
                }
                else if (collider.gameObject.CompareTag("Wizard")) {
                    Wizard wizardScript = collider.gameObject.GetComponent<Wizard>();
                    wizardScript.TakeDamage(100);
                }
            }
            Debug.Log("PLAY");
            GameObject effect = Instantiate(visualEffect, transform.position, transform.rotation);
            Debug.Log("End");
            //Destroy(effect);
            Destroy(gameObject);
        }
        
    }
    
}
