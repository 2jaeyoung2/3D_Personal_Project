using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
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

    private void Start()
    {
        healthBar.value = 1;
        
        player.OnHealthChanged += UpdateHealthBar;

        player.OnEXPChanged += UpdateExpBar;
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
            healthText.text = $"{currentPlayerExp} / {maxExp}";
        }
    }

    private void OnDestroy()
    {
        player.OnHealthChanged -= UpdateHealthBar;

        player.OnEXPChanged -= UpdateExpBar;
    }


}
