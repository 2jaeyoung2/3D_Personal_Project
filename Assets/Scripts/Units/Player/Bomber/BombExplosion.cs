using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    public event Action<float> OnDamageChanged;

    public Player player;

    [SerializeField]
    private ParticleSystem sparkEffect;

    [SerializeField]
    private ParticleSystem explosionEffect;

    [SerializeField]
    private MeshRenderer bomb;

    [SerializeField]
    private float damage;

    private void Start()
    {
        player.OnLevelChanged += IncreaseDamage;

        damage = 30f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            StartCoroutine(Explosion());
        }
    }

    private void IncreaseDamage()
    {
        damage *= 1.1f;

        OnDamageChanged?.Invoke(damage);
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(1f);

        sparkEffect.Stop();

        explosionEffect.Play();

        SoundManager.Instance.PlayBombExplosionSound();

        bomb.enabled = false;

        var damagedEnemies = Physics.OverlapSphere(gameObject.transform.position, 3.5f);

        foreach (var a in damagedEnemies)
        {
            if (a.CompareTag("Enemy"))
            {
                a.GetComponent<IDamageable>()?.GetDamage(damage);
            }
        }

        yield return new WaitUntil(() => explosionEffect.IsAlive() == false);

        Destroy(gameObject); // TODO: ����Ǯ �� �� Return���� �ٲٰ� bomb.enabled�� true�� �ٲ���� ��.
    }
}
