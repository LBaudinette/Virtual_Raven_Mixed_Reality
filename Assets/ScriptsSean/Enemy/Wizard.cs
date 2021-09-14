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

    public EnemyState enemyState;
    public WizardType wizardType;

    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    public float afterSlowDelay = 5.0f;
    public int enemyDamage = 5;
    public float enemyStartingHealth = 100f;
    public float enemyHealth = 100f;
    public int enemyScoreGain = 50;
    public float attackDelay = 1.0f;
    public Canvas enemyCanvas;
    [HideInInspector] public bool deathAnimationDone = false;

    [Header("Unity Stuff")]
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
        PlayerStats.score += enemyScoreGain;
        // play enemy death animation
        // have them sink into the ground and then destroy
        enemyCollider.enabled = false;
        enemyCanvas.enabled = false;
        //Destroy(gameObject);
        StartCoroutine(WaitForDeathAnimation());
        //StartCoroutine(SinkEnemyIntoGround());
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
