using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGhost : EnemyManager
{
    protected override float EnemyHP { get => base.EnemyHP; set => base.EnemyHP = value; }

    protected override float MoveSpeed { get => base.MoveSpeed; set => base.MoveSpeed = value; }

    [SerializeField]
    private GameObject basicGhostProjectile;

    [SerializeField]
    private float cooltime;

    private float tempCoolTime;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }

    protected override void Start()
    {
        EnemyHP = 30f;

        MoveSpeed = 1.4f;

        cooltime = 2.6f;

        tempCoolTime = 0.5f;

        enemyState = EnemyState.Follow;
    }

    protected override void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Follow:

                Move();

                break;

            case EnemyState.Attack:

                Boo();

                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyState = EnemyState.Attack;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyState = EnemyState.Follow;
        }
    }

    protected override void Move()
    {
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
    }

    private void Boo()
    {
        tempCoolTime -= Time.deltaTime;

        if (tempCoolTime <= 0)
        {
            Instantiate(basicGhostProjectile, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);

            tempCoolTime = cooltime;
        }
    }

    public override void Die()
    {
        DropEXP();

        Destroy(gameObject);
    }

    public override void DropEXP()
    {
        Instantiate(gemToDrop, transform.position, transform.rotation);
    }
}
