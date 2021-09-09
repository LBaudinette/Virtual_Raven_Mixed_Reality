using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public SphereCollider collider;
    public float initRad;
    public float finalRad;
    public float maxTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        StartCoroutine(expandExplosion());
    }

    private void OnDisable()
    {
        collider.radius = initRad;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator expandExplosion()
    {
        float elapsed = 0;
        while (elapsed < maxTime)
        {
            elapsed += Time.deltaTime;
            collider.radius = Mathf.Lerp(initRad, finalRad, elapsed / maxTime);
            yield return null;
        }
        this.gameObject.SetActive(false);
    }
}
