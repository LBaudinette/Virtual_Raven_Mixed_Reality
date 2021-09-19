using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpell : MonoBehaviour
{
    public float beamDuration;
    public Transform startingPosition; 
    private LineRenderer lineRenderer;

    private Coroutine coroutine;
    private float beamTimer;
    public LayerMask enemyLayer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startBeam() {
        lineRenderer.enabled = true;
        GetComponent<MeshRenderer>().enabled = false;
        coroutine = StartCoroutine(displayBeam());
    }

    private IEnumerator displayBeam() {
        while(beamTimer < beamDuration) {
            beamTimer += Time.deltaTime;
            lineRenderer.SetPosition(0, startingPosition.position);
            lineRenderer.SetPosition(1, startingPosition.transform.forward * 50);
            Debug.DrawRay(startingPosition.position, startingPosition.transform.forward * 50);
            RaycastHit hit;
            if (Physics.Raycast(startingPosition.position, startingPosition.transform.forward * 50, out hit, Mathf.Infinity, enemyLayer)) {
                if (hit.collider.gameObject.CompareTag("Enemy")) {
                    Enemy enemyScript = hit.collider.gameObject.GetComponent<Enemy>();
                    enemyScript.TakeDamage(2);
                    enemyScript.Slow(50);
                }
            }

            yield return null;
        }

        beamTimer = 0;
        lineRenderer.enabled = false;
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision) {
        
    }
}
