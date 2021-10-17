using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFirerTester : MonoBehaviour
{
    float timer = 0f;
    float timeLimit = 5f;
    public GameObject testSpell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(timer >= timeLimit) {
            GameObject tester = Instantiate(testSpell, gameObject.transform.position, gameObject.transform.rotation);
            tester.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
            timer = 0f;
        }
        else {
            timer += Time.deltaTime;
        }
    }
}
