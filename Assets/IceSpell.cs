using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class IceSpell : MonoBehaviour
{
    public float beamDuration;
    public Transform startingPosition;
    public float beamDamage;
    private LineRenderer lineRenderer;
    private bool isAiming = false;

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
        if (isAiming) {
            lineRenderer.SetPosition(0, gameObject.transform.position);
            lineRenderer.SetPosition(1, startingPosition.transform.forward * 50);
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
        }
    }

    public void startBeam() {
        isAiming = false;
        lineRenderer.enabled = true;
        GetComponent<MeshRenderer>().enabled = false;
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;

        coroutine = StartCoroutine(displayBeam());
    }
    public void startAiming() {
        lineRenderer.enabled = true;
        isAiming = true;
    }

    private IEnumerator displayBeam() {

        while (beamTimer < beamDuration) {
            beamTimer += Time.deltaTime;
            lineRenderer.SetPosition(0, startingPosition.transform.position);
            lineRenderer.SetPosition(1, startingPosition.transform.forward * 50);
            Debug.DrawRay(startingPosition.position, startingPosition.transform.forward * 50);
            RaycastHit hit;
            if (Physics.Raycast(startingPosition.position, startingPosition.transform.forward * 50, out hit, Mathf.Infinity, enemyLayer)) {
                if (hit.collider.gameObject.CompareTag("Enemy")) {
                    Enemy enemyScript = hit.collider.gameObject.GetComponent<Enemy>();
                    enemyScript.TakeDamage(beamDamage);
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
