using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wizard : MonoBehaviour
{
    public enum WizardType
    {
        Fire,
        Ice,
        Ground
    }

    public enum EnemyState
    {
        Moving,
        Attacking,
        Dead,
        Victory
    };

    [Header("Enemy Enums")]
    public EnemyState enemyState;
    public WizardType wizardType;

    [Header("Wizard Type Variables")]
    [SerializeField] private Material[] wizardMaterials;
    [SerializeField] private SkinnedMeshRenderer wizardMesh;

    [Header("Wizard Movement Speed")]
    [SerializeField] private float startSpeed = 10f;
    [HideInInspector] public float speed;
    [SerializeField] private float afterSlowDelay = 5.0f;

    [Header("Damage Variables")]
    public int enemyDamage = 5;
    public float enemyStartingHealth = 100f;
    public float enemyHealth = 100f;
    public int enemyScoreGain = 100;

    [Header("Animation Variables")]
    [HideInInspector] public bool deathAnimationDone = false;
    public float attackDelay = 1.0f;
    public float attackAnimationLength = 1.10f;
    public float beforeAttackDelay = 0.5f;

    [Header("Unity Stuff")]
    public Image healthBar;
    public Canvas enemyCanvas;
    [HideInInspector] public Animator animator;
    private SphereCollider enemyCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<SphereCollider>();
        AssignWizardType();
    }
    private void OnEnable()
    {
        ResetObject();
    }

    private void Start()
    {

        speed = startSpeed;
        enemyHealth = enemyStartingHealth;
        enemyState = EnemyState.Moving;
    }

    public void ResetObject()
    {
        AssignWizardType();
        enemyHealth = enemyStartingHealth;
        healthBar.fillAmount = enemyHealth / enemyStartingHealth;
        transform.localPosition = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
        enemyState = EnemyState.Moving;

        speed = startSpeed;
        animator.SetBool("IsDead", false);
        enemyCollider.enabled = true;
        enemyCanvas.enabled = true;
        deathAnimationDone = false;
    }

    private void AssignWizardType()
    {
        // assign wizard type enum and material
        int randomWizardInt = Random.Range(0, 3);
        wizardType = (WizardType)randomWizardInt;
        wizardMesh.material = wizardMaterials[randomWizardInt];
    }

    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;

        healthBar.fillAmount = enemyHealth / enemyStartingHealth;

        if (enemyHealth <= 0 && enemyState != EnemyState.Dead)
        {
            Die();
        }
    }

    private void Die()
    {
        enemyState = EnemyState.Dead;
        animator.SetBool("IsDead", true);
        PlayerStats.AddToScore(enemyScoreGain);
        // have them sink into the ground and then destroy
        enemyCollider.enabled = false;
        enemyCanvas.enabled = false;
        StartCoroutine(WaitForDeathAnimation());
    }

    IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(afterSlowDelay);
        deathAnimationDone = true;
        StartCoroutine(SinkEnemyIntoGround());
    }

    IEnumerator SinkEnemyIntoGround()
    {
        yield return new WaitForSeconds(afterSlowDelay);
        gameObject.SetActive(false);
    }
}
