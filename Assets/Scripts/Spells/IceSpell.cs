using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.VFX;
public class IceSpell : MonoBehaviour
{
    public float beamDuration;
    //public Transform startingPosition;
    [SerializeField]
    private Transform handTransform;
    public float beamDamage;
    public LineRenderer lineRenderer;
    public GameObject particles;
    public GameObject mist;

    private bool isAiming = false;
    public Rigidbody rb;

    private Coroutine coroutine;
    private float beamTimer;
    public LayerMask enemyLayer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.enabled = false;
        particles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming) {
            //lineRenderer.SetPosition(0, handTransform.position);
            //lineRenderer.SetPosition(1, handTransform.forward * 50);
            //lineRenderer.startColor = Color.white;
            //lineRenderer.endColor = Color.white;
        }
    }

    public void startBeam() {
        isAiming = false;
        lineRenderer.enabled = true;
        particles.SetActive(true);
        mist.SetActive(false);
        GetComponentInChildren<MeshRenderer>().enabled = false;
        rb.isKinematic = true;


        coroutine = StartCoroutine(displayBeam());
    }
    public void startAiming() {
        //lineRenderer.enabled = true;
        isAiming = true;
        Debug.Log("GET TRANSFORM BEAM");
        handTransform = transform.parent.GetChild(1);
        Debug.Log("CHILD NAME: " + handTransform);
    }

    private IEnumerator displayBeam() {
        while (beamTimer < beamDuration) {

            beamTimer += Time.deltaTime;
            lineRenderer.SetPosition(0, handTransform.position);
            lineRenderer.SetPosition(1, handTransform.position + handTransform.forward * 50);

            particles.transform.position = handTransform.position;
            particles.transform.rotation = handTransform.rotation;

            //Debug.DrawRay(handTransform.position, handTransform.forward * 50);
            RaycastHit hit;
            if (Physics.Raycast(handTransform.position, handTransform.forward * 50, out hit, Mathf.Infinity, enemyLayer)) {
                if (hit.collider.gameObject.CompareTag("Enemy")) {
                    Enemy enemyScript = hit.collider.gameObject.GetComponent<Enemy>();
                    enemyScript.TakeDamage(beamDamage * Time.deltaTime);
                    enemyScript.Slow(50);
                } else if (hit.collider.gameObject.CompareTag("Wizard")) {
                    Wizard wizardScript = hit.collider.gameObject.GetComponent<Wizard>();
                    wizardScript.TakeDamage(beamDamage * Time.deltaTime);
                }
            }

            yield return null;
        }

        beamTimer = 0;
        lineRenderer.enabled = false;
        Destroy(gameObject);
    }
}
