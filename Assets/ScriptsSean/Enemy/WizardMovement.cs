using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Wizard))]
public class WizardMovement : MonoBehaviour
{
    public Transform[] patrolPoints;   // 2 waypoints for each spawn
    public Transform target;
    private Wizard wizard;

    public float distanceToCurrentPatrolPoint;
    public int patrolPointIndex;

    //turning  variables
    private Coroutine lookCoroutine;
    public float rotationSpeed = 1.0f;
    public float excitation = 2.0f;
    public bool hasAttacked = false;
    public bool isAttacking = false;

    private void Awake()
    {
        wizard = GetComponent<Wizard>();
        patrolPointIndex = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //transform.LookAt(patrolPoints[patrolPointIndex].position);
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
            wizard.animator.SetBool("GateDestoryed", true);
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
            Debug.Log(playerAngle);
            if (playerAngle > 1.0f)
            {
                // The step size is equal to speed times frame time.
                float step = rotationSpeed * Time.deltaTime;

                //// Rotate our transform a step closer to the target's.
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, DummyPlayerScript.playerPosition.rotation, step);

                Vector3 lookVector = DummyPlayerScript.playerPosition.position - transform.position;
                //lookVector.y = transform.position.y;
                Quaternion rotation = Quaternion.LookRotation(lookVector);
                //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
                // Rotate our transform a step closer to the target's.
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, step);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
        }
        else
        {
            float angle = Quaternion.Angle(transform.rotation, patrolPoints[patrolPointIndex].rotation);
            //Debug.Log(angle);

            //turn until facing current patrol point
            if (angle > 1.0f)
            {
                // The step size is equal to speed times frame time.
                float step = rotationSpeed * Time.deltaTime;

                // Rotate our transform a step closer to the target's.
                transform.rotation = Quaternion.RotateTowards(transform.rotation, patrolPoints[patrolPointIndex].rotation, step);
            }
            else
            {
                // line up rotation exactly to prevent unwanted movement in the z dimension while patrolling
                transform.rotation = patrolPoints[patrolPointIndex].rotation;


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
        wizard.animator.SetBool("Attacking", true);
        Invoke(nameof(FireSpell), wizard.attackDelay);
    }

    private void FireSpell()
    {
        Debug.Log("Attack");

        // --instantiate spell here--


        //

        Invoke(nameof(FinishAttack), wizard.attackDelay);
    }

    private void FinishAttack()
    {
        hasAttacked = true;
        isAttacking = false;
        wizard.animator.SetBool("Attacking", false);
    }
}
