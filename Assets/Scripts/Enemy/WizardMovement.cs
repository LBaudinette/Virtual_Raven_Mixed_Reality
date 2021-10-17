using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Wizard))]
public class WizardMovement : MonoBehaviour
{
    [Header("Reference to Scene Componenents")]
    [SerializeField] private Transform[] patrolPoints;   // 2 waypoints for each spawn
    [SerializeField] private Transform target;
    private Wizard wizard;

    [Header("Movement Variables")]
    [SerializeField] private float distanceToCurrentPatrolPoint;
    [SerializeField] private int patrolPointIndex;
    private Coroutine lookCoroutine;
    [SerializeField] private float rotationSpeed = 2.0f;
    private bool hasAttacked = false;
    private bool isAttacking = false;

    public GameObject fireSpell;
    public GameObject iceSpell;
    public GameObject earthSpell;
    public Transform spellStartingPoint;

    private void Awake()
    {
        wizard = GetComponent<Wizard>();
        patrolPointIndex = 0;
    }

    private void OnEnable()
    {
        patrolPointIndex = 0;
        distanceToCurrentPatrolPoint = 0;
        hasAttacked = false;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (wizard.enemyState == Wizard.EnemyState.Dead)
        {
            if (wizard.deathAnimationDone)
            {
                //transform.Translate(Vector3.down.y * enemy.speed * Time.deltaTime, 0, 0, Space.World);
                transform.Translate(0, Vector3.down.y * wizard.speed * Time.deltaTime, 0, Space.World);
            }
            return;
        }

        if (wizard.enemyState == Wizard.EnemyState.Victory)
        {
            return;
        }

        if (GameManager.gameIsOver)
        {
            wizard.animator.SetBool("GateDestroyed", true);
            wizard.enemyState = Wizard.EnemyState.Victory;
            return;
        }

        // move to ledge between two patrol points
        if (transform.position.z > target.position.z)
        {
            transform.Translate(0, 0, Vector3.back.z * wizard.speed * Time.deltaTime, Space.World);
            return;
        }

        if (isAttacking)
        {
            float playerAngle = Quaternion.Angle(transform.rotation, DummyPlayerScript.playerPosition.rotation);
            //Debug.Log(playerAngle);
            if (playerAngle > 1.0f)
            {
                // The step size is equal to speed times frame time.
                float step = rotationSpeed * Time.deltaTime;

                Vector3 lookVector = DummyPlayerScript.playerPosition.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(lookVector);

                // Rotate our transform a step closer to the target's.
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, step);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
        }
        else
        {
            float angle = Quaternion.Angle(transform.rotation, patrolPoints[patrolPointIndex].rotation);
            Debug.Log("Angle towards patrol point " + patrolPointIndex + ": " + angle + ". hasAttacked = " + hasAttacked);

            //turn until facing current patrol point
            if (angle > 1.0f && hasAttacked == false)
            {
                // The step size is equal to speed times frame time.
                float step = rotationSpeed * Time.deltaTime;

                // Rotate our transform a step closer to the target's.
                transform.rotation = Quaternion.RotateTowards(transform.rotation, patrolPoints[patrolPointIndex].rotation, step);
            }
            else
            {
                if (!hasAttacked)
                {
                    // line up rotation exactly to prevent unwanted movement in the z dimension while patrolling
                    transform.rotation = patrolPoints[patrolPointIndex].rotation;
                }

                distanceToCurrentPatrolPoint = Vector3.Distance(transform.position, patrolPoints[patrolPointIndex].position);

                // check if at control point
                if (distanceToCurrentPatrolPoint < 1.5f)
                {
                    // check if the wizard has attacked after stopping at the point
                    if (isAttacking == false && hasAttacked == false)
                    {
                        isAttacking = true;
                        AttackPlayer();
                    }

                    // start moving to next control point
                    if (hasAttacked)
                    {
                        IncreaseIndex();
                        hasAttacked = false;
                    }
                }
                else
                {
                    Patrol();
                }
            }
        }
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * wizard.speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        patrolPointIndex++;
        if (patrolPointIndex >= patrolPoints.Length)
        {
            patrolPointIndex = 0;
        }
    }

    public void StartRotating()
    {
        if (lookCoroutine != null)
        {
            StopCoroutine(lookCoroutine);
        }
        lookCoroutine = StartCoroutine(LookAt());
    }

    private IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(patrolPoints[patrolPointIndex].position - transform.position);
        //Debug.Log(lookRotation);
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            //Debug.Log(transform.rotation);
            time += Time.deltaTime * rotationSpeed;

            yield return null;
        }
    }

    private void AttackPlayer()
    {
        StartCoroutine(FireSpell());
    }

    IEnumerator FireSpell()
    {
        // wait for wizard to finish moving to position
        yield return new WaitForSeconds(wizard.attackDelay);
        wizard.animator.SetBool("Attacking", true);

        // wait for attack animation to reach the exact point where it should be firing a spell
        yield return new WaitForSeconds(wizard.beforeAttackDelay);
        //Debug.Log("Attack");

        // --instantiate spell here--
        switch (wizard.wizardType) {

            case Wizard.WizardType.Fire:
                Instantiate(fireSpell, transform.position, transform.rotation);
                break;
            case Wizard.WizardType.Ice:
                Instantiate(iceSpell, transform.position, transform.rotation);
                break;
            case Wizard.WizardType.Ground:
                Instantiate(earthSpell, transform.position, transform.rotation);
                break;
        }
        // --

        // wait until attack animation is done
        yield return new WaitForSeconds(wizard.attackAnimationLength);
        StartCoroutine(FinishAttack());
    }

    IEnumerator FinishAttack()
    {
        hasAttacked = true;
        isAttacking = false;
        wizard.animator.SetBool("Attacking", false);
        yield return null;
    }
}
