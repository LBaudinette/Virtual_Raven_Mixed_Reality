using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpell : MonoBehaviour
{
    public Transform startingPosition;
    private LineRenderer lineRenderer;
    private bool isAiming = false;

    public LayerMask enemyLayer;
    public float firingForce;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming) {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.forward * 50);
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
        }
    }
    
    public void fireSpell() {
        lineRenderer.enabled = false;
        isAiming = false;
        //GetComponent<SphereCollider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * firingForce, ForceMode.Impulse);
    }

    public void startAiming() {
        lineRenderer.enabled = true;
        isAiming = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Debug.Log("ENEMY HIT: DESTROY");
            other.gameObject.GetComponent<Enemy>().TakeDamage(50);
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            Debug.Log("GROUND HIT: DESTROY");
            Destroy(gameObject);
        }
    }
}
