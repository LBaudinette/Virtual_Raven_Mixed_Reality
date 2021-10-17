using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class IceSpell : MonoBehaviour
{
    public float beamDuration;
    //public Transform startingPosition;
    [SerializeField]
    private Transform handTransform;
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
       // handTransform = GameObject.FindWithTag("RHSAttatchment").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming) {
            lineRenderer.SetPosition(0, handTransform.position);
            lineRenderer.SetPosition(1, handTransform.forward * 50);
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
        GetComponent<Rigidbody>().isKinematic = true;


        coroutine = StartCoroutine(displayBeam());
    }
    public void startAiming() {
        lineRenderer.enabled = true;
        isAiming = true;
        handTransform = transform.parent.GetChild(1);
        Debug.Log("CHILD NAME: " + transform.GetChild(1));
    }

    private IEnumerator displayBeam() {
        while (beamTimer < beamDuration) {
            beamTimer += Time.deltaTime;
            lineRenderer.SetPosition(0, handTransform.position);
            lineRenderer.SetPosition(1, handTransform.forward * 50);
            Debug.DrawRay(handTransform.position, handTransform.forward * 50);
            RaycastHit hit;
            if (Physics.Raycast(handTransform.position, handTransform.forward * 50, out hit, Mathf.Infinity, enemyLayer)) {
                if (hit.collider.gameObject.CompareTag("Enemy")) {
                    Enemy enemyScript = hit.collider.gameObject.GetComponent<Enemy>();
                    enemyScript.TakeDamage(beamDamage * Time.deltaTime);
                    enemyScript.Slow(50);
                }
            }

            yield return null;
        }

        beamTimer = 0;
        lineRenderer.enabled = false;
        Destroy(gameObject);
    }
}
