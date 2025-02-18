using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackMagician : MonoBehaviour
{
    [SerializeField]
    private GameObject card;

    [SerializeField]
    private float cooltime;

    private float tempCoolTime;

    [SerializeField]
    [Range(3, 6)]
    private int cardNumber;

    private void Start()
    {
        cooltime = 3f;

        tempCoolTime = cooltime;

        cardNumber = 3;
    }

    void Update()
    {
        tempCoolTime -= Time.deltaTime;

        if (tempCoolTime <= 0f)
        {
            StartCoroutine(ThroughCards(cardNumber));

            tempCoolTime = cooltime;
        }
    }

    IEnumerator ThroughCards(int count)
    {
        for (int i = count; i > 0; i--)
        {
            Instantiate(card, transform.position + new Vector3(0, 0.2f, 0), transform.rotation);

            yield return new WaitForSeconds(0.3f);
        }
    }
}