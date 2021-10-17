using UnityEngine;

public class SpellStorage : MonoBehaviour
{
    GameObject spellOne;
    GameObject spellTwo;
    GameObject spellThree;

    public GameObject spellSlots;
    public GameObject testSpell;
    public enum Spell {
        fire, ice, earth
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addSpell(Spell newSpell) {
        if (spellOne == null)
            createSpell(newSpell, spellOne.transform.position);
        else if (spellTwo == null)
            createSpell(newSpell, spellTwo.transform.position);
        else if(spellThree == null)
            createSpell(newSpell, spellThree.transform.position);

        //If both slots are full then don't add this new spell
    }

    private void createSpell(Spell newSpell, Vector3 position) {
        //Instantiate(testSpell,gam)
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"COLLISION NAME: {other.gameObject.name}");
        if (other.CompareTag("Spell")) {
            GameObject collisionObject = other.gameObject;
            collisionObject.GetComponent<Rigidbody>().useGravity = false;
            collisionObject.transform.position = spellSlots.transform.position;
            
        }
    }
}
