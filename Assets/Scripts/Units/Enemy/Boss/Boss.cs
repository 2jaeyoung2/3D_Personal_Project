using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

public enum Phase
{
    Blue, Pink, Red, Dead
}

public class Boss : Enemy
{
    public event Action<float, float> OnHealthChanged;

    public event Action<EnemyState> OnStateChanged;

    public event Action<Phase> OnPhaseChanged;

    [SerializeField]
    private Timer timer;

    Phase bossPhase;

    protected override float EnemyHP { get => base.EnemyHP; set => base.EnemyHP = value; }

    protected override float MoveSpeed { get => base.MoveSpeed; set => base.MoveSpeed = value; }

    private float bossCurrentHP;

    [SerializeField]
    private GameObject portal;

    private float elapsedTime = 0;

    protected override void Start()
    {
        timer.IsTimeEnd += SetBoss;

        gameObject.SetActive(false);

        EnemyHP = 2700f;

        bossCurrentHP = EnemyHP;

        MoveSpeed = 0.6f;

        enemyState = EnemyState.Follow;

        OnHealthChanged?.Invoke(bossCurrentHP, EnemyHP);
    }

    protected override void Update()
    {
        elapsedTime += Time.deltaTime;

        switch (enemyState)
        {
            case EnemyState.Follow:

                OnStateChanged?.Invoke(enemyState);

                Move();

                break;

            case EnemyState.Attack:

                if (elapsedTime > 3.2f) // 3.2 �ʸ��� ����
                {
                    OnStateChanged?.Invoke(enemyState);

                    elapsedTime = 0;
                }

                break;
        }
    }

    private void OnTriggerStay(Collider other)
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

    private void SetBoss()
    {
        gameObject.SetActive(true);
    }

    public override void GetDamage(float damage)
    {
        bossCurrentHP -= damage;

        // ������ ���� �� ���� ������ üũ
        if (EnemyHP * (2f / 3f) > bossCurrentHP && bossCurrentHP >= EnemyHP / 3f)
        {
            bossPhase = Phase.Pink;
        }
        if(EnemyHP / 3f > bossCurrentHP && bossCurrentHP > 0)
        {
            bossPhase = Phase.Red;
        } 
        if( 0 >= bossCurrentHP)
        {
            bossPhase = Phase.Dead;
        }

        OnHealthChanged?.Invoke(bossCurrentHP, EnemyHP);

        OnPhaseChanged?.Invoke(bossPhase); // üũ�� ������ �����ֱ�

        if (bossCurrentHP <= 0)
        {
            bossCurrentHP = 0;

            OnHealthChanged?.Invoke(bossCurrentHP, EnemyHP);

            Die();
        }
    }

    public override void Die()
    {
        StartCoroutine(GoToHell());
    }

    IEnumerator GoToHell()
    {
        CapsuleCollider colliderToDel = gameObject.GetComponent<CapsuleCollider>();

        Destroy(colliderToDel); // �ٴ� �Ʒ��� ���������� �ݶ��̴� ����

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        rb.useGravity = false; // �߷� �ۿ� ���ϵ��� �߷� ����

        playerToChase = null; // �÷��̾� �ٶ󺸱� ����

        //��Ż ����
        portal = Instantiate(portal, new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.Euler(90, 0, 0));

        while (gameObject.transform.position.y > -2.7f)
        {
            // �ٴ����� ������ ���������
            gameObject.transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);

            yield return null;
        }

        Destroy(portal);

        gameObject.SetActive(false);
    }

    public override void DropEXP()
    {

    }

    public override void DropHEAL()
    {

    }
}