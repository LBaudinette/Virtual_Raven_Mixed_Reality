using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSpellScript : MonoBehaviour
{

    GameObject player;
    public float speed;
    public float lifeTimer = 0;
    public float lifeLimit = 6;
    public int damageToPlayer = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeLimit) {
            Destroy(gameObject);
        }
        else {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<DummyPlayerScript>().TakeDamage(10);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerStats.MinusPlayerHealth(damageToPlayer);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }
}
