using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaticUI : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private Slider healthBar;

    [SerializeField]
    private TextMeshProUGUI healthText;

    [SerializeField]
    private Slider expBar;

    [SerializeField]
    private TextMeshProUGUI expText;

    [SerializeField]
    private BombDrop bomb;

    [SerializeField]
    private Image qCoolTimeIcon;

    [SerializeField]
    private TextMeshProUGUI coolTimeText;

    // TODO: 중앙 상단에 시간

    private void Start()
    {
        healthBar.value = 1;
        
        player.OnHealthChanged += UpdateHealthBar;

        player.OnEXPChanged += UpdateExpBar;

        bomb.OnSkillUsed += UpdateCoolTime;
    }

    private void UpdateHealthBar(float currentHP, float maxHP)
    {
        if (healthBar != null)
        {
            healthBar.value = currentHP / maxHP;
        }

        if (healthText != null)
        {
            healthText.text = $"{currentHP} / {maxHP}";
        }
    }

    private void UpdateExpBar(float currentPlayerExp, float maxExp)
    {
        if (expBar != null)
        {
            expBar.value = currentPlayerExp / maxExp;
        }

        if (expText != null)
        {
            expText.text = $"{currentPlayerExp} / {maxExp}";
        }
    }

    private void UpdateCoolTime(float leftTime, float coolTime)
    {
        qCoolTimeIcon.fillAmount = leftTime / coolTime;

        if (Mathf.Abs(qCoolTimeIcon.fillAmount) < 0.99f)
        {
            coolTimeText.text = $"{(int)coolTime - (int)leftTime}";
        }
        else
        {
            coolTimeText.text = null;
        }
    }

    private void OnDestroy()
    {
        player.OnHealthChanged -= UpdateHealthBar;

        player.OnEXPChanged -= UpdateExpBar;

        bomb.OnSkillUsed -= UpdateCoolTime;
    }
}
