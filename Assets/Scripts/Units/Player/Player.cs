using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IExp, IHeal
{
    public event Action<float, float> OnHealthChanged;

    public event Action<float, float> OnEXPChanged;

    public event Action<float> OnMaxHealthChanged;

    public event Action OnGetHP;

    public event Action OnLevelChanged;

    public event Action IsDead;

    [SerializeField]
    private GameObject gameManager;

    [SerializeField]
    private float playerMaxHP;

    private float currentHP;

    [SerializeField]
    private float currentPlayerEXP;

    private int level;

    void Start()
    {
        playerMaxHP = 40f;

        currentHP = playerMaxHP;

        level = 1;

        currentPlayerEXP = 0f;

        OnHealthChanged?.Invoke(currentHP, playerMaxHP);

        OnEXPChanged?.Invoke(currentPlayerEXP, RequiredExp(level));

        OnLevelChanged += IncreaseHP;
    }

    public void GetDamage(float damage)
    {
        currentHP -= damage;

        OnHealthChanged?.Invoke(currentHP, playerMaxHP);

        if (currentHP <= 0)
        {
            Debug.Log("Dead");

            currentHP = 0;

            OnHealthChanged?.Invoke(currentHP, playerMaxHP);

            Die();
        }
    }

    private void IncreaseHP()
    {
        playerMaxHP += 15f;

        OnMaxHealthChanged?.Invoke(playerMaxHP);
    }

    public void GetHealth(float heal)
    {
        SoundManager.Instance.PlayHealSound();

        if (currentHP < playerMaxHP)
        {
            currentHP += heal;

            if (currentHP > playerMaxHP)
            {
                currentHP = playerMaxHP;
            }
        }

        OnHealthChanged?.Invoke(currentHP, playerMaxHP);

        OnGetHP?.Invoke();
    }

    public void GetExp(float exp)
    {
        currentPlayerEXP += exp;

        // ���� ��
        if (currentPlayerEXP >= RequiredExp(level))
        {
            SoundManager.Instance.PlayLevelUpSound();

            level++;

            OnLevelChanged?.Invoke();
        }

        OnEXPChanged?.Invoke(currentPlayerEXP, RequiredExp(level));
    }

    public void Die()
    {
        IsDead?.Invoke();

        //gameObject.SetActive(false);

        Destroy(gameObject);
    }

    public float RequiredExp(int level) // ���� ������ ���� ������ �䱸 ����ġ ��
    {
        if (level == 1) return 10f;

        if (level == 2) return 25f;

        return RequiredExp(level - 1) + RequiredExp(level - 2);
    }
}
