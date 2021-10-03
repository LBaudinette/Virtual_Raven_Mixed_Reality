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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkSlots() {
        if(spellOneTransform.childCount == 0) {
            spellOneEmpty = true;
        }
        if (spellOneTransform.childCount == 0) {
            spellTwoEmpty = true;

        }
    }

    public void addSpell(SpellType newSpell) {
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
        switch (newSpell) {
            case SpellType.earth:
                Instantiate(earthSpell, positionTransform);
                break;
            case SpellType.ice:
                Instantiate(iceSpell, positionTransform);
                break;
            case SpellType.fire:
                Instantiate(fireSpell, positionTransform);
                break;
        }
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
