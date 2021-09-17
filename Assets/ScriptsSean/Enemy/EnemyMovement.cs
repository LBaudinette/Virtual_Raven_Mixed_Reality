using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Enemy enemy;
    private bool alreadyAttacked = false;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        target = WayPoint.point;
    }

    private void Update()
    {
        if(enemy.enemyState == Enemy.EnemyState.Dead)
        {
            if (enemy.deathAnimationDone)
            {
                transform.Translate(0, Vector3.down.y * enemy.speed * Time.deltaTime, 0, Space.World);
            }
            return;
        }

        if (enemy.enemyState == Enemy.EnemyState.Victory)
        {
            return;
        }

        if (GameManager.gameIsOver)
        {
            enemy.enemyState = Enemy.EnemyState.Victory;
            enemy.animator.SetBool("GateDestroyed", true);
            return;
        }

        // if not at gate move to target
        if (transform.position.x < target.position.x)
        {
            enemy.animator.SetBool("Walking", false);
            if (!alreadyAttacked)
            {
                StartCoroutine(AttackGate());
            }
        }
        else
        {
            transform.Translate(Vector3.left.x * enemy.speed * Time.deltaTime, 0, 0, Space.World);
        }
    }

    IEnumerator AttackGate()
    {
            alreadyAttacked = true;
            enemy.animator.SetBool("Attacking", true);

            // wait for enemy to stab the gate
            yield return new WaitForSeconds(enemy.timeBeforeAttack);

            // damage the gate
            PlayerStats.MinusGateHealth(enemy.enemyDamage);

            //Debug.Log(PlayerStats.gatehealth);
            
            // wait until enemy can attack
            yield return new WaitForSeconds(enemy.timeBetweenAttacks);
            ResetAttack();
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        enemy.animator.SetBool("Attacking", false);
    }
}
