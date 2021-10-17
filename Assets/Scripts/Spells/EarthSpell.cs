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
            lineRenderer.SetPosition(0, startingPosition.position);
            lineRenderer.SetPosition(1, startingPosition.position + startingPosition.forward * 50);
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
        }
    }
    
    public void fireSpell() {
        lineRenderer.enabled = false;
        isAiming = false;
        //GetComponent<SphereCollider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(startingPosition.forward * firingForce, ForceMode.Impulse);
    }

    public void startAiming() {
        lineRenderer.enabled = true;
        isAiming = true;
        startingPosition = transform.parent.GetChild(1);

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<Enemy>().TakeDamage(100);
            Destroy(gameObject);
        } else if (other.gameObject.CompareTag("Wizard")) {
            other.gameObject.GetComponent<Wizard>().TakeDamage(100);
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
