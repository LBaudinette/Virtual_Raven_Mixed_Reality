using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Moving,
        Attacking,
        Dead,
        Victory
    };

    [Header("Enemy Enums")]
    public EnemyState enemyState;

    [Header("Enemy Movement Speed")]
    [SerializeField] private float startSpeed = 10f;
    [HideInInspector] public float speed;
    [SerializeField] private float afterSlowDelay = 5.0f;

    [Header("Damage Variables")]
    public int enemyDamage = 5;
    [SerializeField] private float enemyStartingHealth = 100f;
    [SerializeField] private float enemyHealth = 100f;
    [SerializeField] private int enemyScoreGain = 50;

    [Header("Animation Variables")]
    public float timeBetweenAttacks = 4.0f;
    public float timeBeforeAttack = 1.0f;
    [HideInInspector] public bool deathAnimationDone = false;

    [Header("Unity Stuff")]
    [SerializeField] private Canvas enemyCanvas;
    public Image healthBar;
    [HideInInspector] public Animator animator;
    private SphereCollider enemyCollider;


    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<SphereCollider>();
        speed = startSpeed;
        enemyHealth = enemyStartingHealth;
        enemyState = EnemyState.Moving;
        animator.SetBool("Walking", true);
    }

    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;

        healthBar.fillAmount = (float)enemyHealth / (float)enemyStartingHealth;

        if(enemyHealth <= 0 && enemyState != EnemyState.Dead)
        {
            Die();
        }
    }

    public void Slow(float slowPercentage)
    {
        speed = startSpeed * (1f - slowPercentage);
        StartCoroutine(WaitForSlowToStop());
    }

    //Wait for 5 seconds 
    IEnumerator WaitForSlowToStop()
    {
        yield return new WaitForSeconds(afterSlowDelay);
        speed = startSpeed;
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
        Destroy(gameObject);
    }
}
