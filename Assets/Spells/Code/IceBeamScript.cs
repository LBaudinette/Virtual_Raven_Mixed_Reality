using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBeamScript : MonoBehaviour
{
    bool fired;
    Vector3 fireAngle;
    float beamRadius;
    public float beamRadiusMax;
    public float easeInTime;
    public float easeOutTime;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        fired = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (fired)
        {
            RaycastHit hit;
            //update angle of fire based on VR controller
            // fireAngle = vrcontroller.transform.eulerangles or w/e
            Physics.SphereCast(this.transform.position, beamRadius, fireAngle, out hit, Mathf.Infinity);    // change Mathf.Infinity? also add layermask to only hit enemies
        }
    }

    void OnFired()
    {
        fired = true;
        StartCoroutine(IceBeamCoRoutine());
    }

    IEnumerator IceBeamCoRoutine()  // may be able to exclude changing beam radius depending on how particle system works
    {
        float elapsed = 0.0f;

        /*while (elapsed < easeInTime)
        {
            elapsed += Time.deltaTime;
            beamRadius = Mathf.Lerp(0.0f, beamRadiusMax, elapsed / easeInTime);
            yield return null;
        }*/

        elapsed = 0.0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        /*elapsed = 0.0f;
        while (elapsed < easeInTime)
        {
            elapsed += Time.deltaTime;
            beamRadius = Mathf.Lerp(0.0f, beamRadiusMax, elapsed / easeInTime);
            yield return null;
        }*/

    }
}
