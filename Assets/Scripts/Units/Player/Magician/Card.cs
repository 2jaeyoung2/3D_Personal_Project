using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Card : MonoBehaviour
{
    public event Action<float> OnDamageChanged;

    public Player player;

    [SerializeField]
    [Range(2f, 10f)]
    private float moveSpeed;

    [SerializeField]
    private float damage;

    private float duration;

    private float elapsedTime;

    private void Awake()
    {
        moveSpeed = 5f;

        damage = 10f;

        duration = 3f;
    }

    private void Start()
    {
        player.OnLevelChanged += IncreaseDamage;
    }

    private void OnEnable()
    {
        elapsedTime = 0;

        StartCoroutine(CardMove());
    }

    IEnumerator CardMove()
    {
        while (duration - elapsedTime > 0)
        {
            elapsedTime += Time.deltaTime;

            gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            yield return null;
        }

        CardPoolManager.Instance.ReturnCard(this);
    }

    private void IncreaseDamage()
    {
        damage *= 1.1f;

        OnDamageChanged?.Invoke(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IDamageable>()?.GetDamage(damage);

            CardPoolManager.Instance.ReturnCard(this);
        }
    }
}
