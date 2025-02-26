﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Follow, Attack
}

public abstract class Enemy : MonoBehaviour, IDamageable
{
    protected EnemyState enemyState;

    protected GameObject playerToChase;

    protected Gem gemToDrop;

    [SerializeField]
    protected float enemyHP;

    protected virtual float EnemyHP
    {
        get
        {
            return enemyHP;
        }
        set
        {
            enemyHP = value;
        }
    }

    [SerializeField]
    protected float moveSpeed;

    protected virtual float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
        set
        {
            moveSpeed = value;
        }
    }

    protected void Awake()
    {
        playerToChase = GameObject.FindWithTag("Player");
    }

    protected abstract void Start(); // playerToChase = GameObject.FindWithTag("Player"); 필수 작성

    protected abstract void Update();

    protected void FixedUpdate()
    {
        FaceToPlayer(playerToChase);
    }


    protected void FaceToPlayer(GameObject player)
    {
        transform.LookAt(player.transform.position);
    }

    protected abstract void Move();

    public virtual void GetDamage(float damage)
    {
        EnemyHP -= damage;

        if (enemyHP <= 0)
        {
            Die();
        }
    }

    public abstract void Die();

    public abstract void DropEXP();
}
