using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackMagician : MonoBehaviour
{
    [SerializeField]
    private float cooltime;

    private float tempCoolTime;

    [SerializeField]
    [Range(3, 6)]
    private int cardNumber;

    private void Start()
    {
        cooltime = 3f;

        tempCoolTime = cooltime / 2;

        cardNumber = 3;
    }

    void Update()
    {
        tempCoolTime -= Time.deltaTime;

        if (tempCoolTime <= 0f)
        {
            StartCoroutine(ThrowCards(cardNumber));

            tempCoolTime = cooltime;
        }
    }

    IEnumerator ThrowCards(int count)
    {
        for (int i = 0; i < count; i++) 
        {
            var card = CardPoolManager.Instance.GetCard();

            card.transform.position = gameObject.transform.position + new Vector3(0, 0.2f, 0);

            yield return new WaitForSeconds(0.2f);
        }
    }
}