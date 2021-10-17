using UnityEngine;

public class SpellStorage : MonoBehaviour
{
    public Transform spellOneTransform;
    public Transform spellTwoTransform;

    private bool spellOneEmpty;
    private bool spellTwoEmpty;

    public GameObject earthSpell;
    public GameObject fireSpell;
    public GameObject iceSpell;

    public enum SpellType {
        fire, ice, earth
    }
    // Start is called before the first frame update
    void Start()
    {
        spellOneEmpty = true;
        spellTwoEmpty = true;
    }

    // Update is called once per frame
    void Update()
    {
        checkSlots();
    }

    public void checkSlots() {
        if(spellOneTransform.childCount == 0) {
            spellOneEmpty = true;
        }
        if (spellTwoTransform.childCount == 0) {
            spellTwoEmpty = true;

        }
    }

    public void addSpell(SpellType newSpell) {
        Debug.Log(newSpell.ToString());
        if (spellOneEmpty) {
            spellOneEmpty = false;
            createSpell(newSpell, spellOneTransform);
        }
        else if (spellTwoEmpty) {
            spellTwoEmpty = false;
            createSpell(newSpell, spellTwoTransform);
        }
        //If both slots are full then don't add this new spell
    }

    private void createSpell(SpellType newSpell, Transform positionTransform) {
        Debug.Log("SPELL STORED");

        GameObject newSpellObject = null;
        switch (newSpell) {
            case SpellType.earth:
                newSpellObject = Instantiate(earthSpell, positionTransform);
                break;
            case SpellType.ice:
                newSpellObject =  Instantiate(iceSpell, positionTransform);
                break;
            case SpellType.fire:
                newSpellObject = Instantiate(fireSpell, positionTransform);
                break;
        }
        newSpellObject.GetComponent<Rigidbody>().isKinematic = true;


    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"COLLISION NAME: {other.gameObject.name}");
        switch (other.gameObject.tag) {
            case "IceWizardSpell":
                addSpell(SpellType.ice);
                break;
            case "EarthWizardSpell":
                addSpell(SpellType.earth);
                break;
            case "FireWizardSpell":
                addSpell(SpellType.fire);
                break;
        }

        //Destroy the spell that collided with it
        Destroy(other.gameObject);
    }
}
