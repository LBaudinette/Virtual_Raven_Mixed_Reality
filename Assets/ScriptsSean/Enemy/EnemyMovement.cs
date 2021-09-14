using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    //private int wavePointIndex = 0;

    private Enemy enemy;
    private bool alreadyAttacked = false;


    public float timeBetweenAttacks = 6.0f;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        target = WayPoint.point;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(enemy.enemyState == Enemy.EnemyState.Dead)
        {
            if (enemy.deathAnimationDone)
            {
                //transform.Translate(Vector3.down.y * enemy.speed * Time.deltaTime, 0, 0, Space.World);
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

        if (transform.position.x < target.position.x)
        {
            enemy.animator.SetBool("Walking", false);
            AttackGate();
        }
        else
        {
            //Vector3 direction = target.position - transform.position;
            //transform.Translate(direction.normalized.x * Speed * Time.deltaTime, 0, 0, Space.World);
            //float direction = target.position.x - transform.position.x;

            transform.Translate(Vector3.left.x * enemy.speed * Time.deltaTime, 0, 0, Space.World);
        }

        //enemy.speed = enemy.startSpeed;
    }

    private void AttackGate()
    {
        //
        //return;

        if (!alreadyAttacked)
        {
            enemy.animator.SetBool("Attacking", true);
            PlayerStats.MinusGateHealth(enemy.enemyDamage);
            Debug.Log(PlayerStats.gatehealth);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        enemy.animator.SetBool("Attacking", false);
    }
}
