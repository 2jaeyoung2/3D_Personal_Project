﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackWarrior : MonoBehaviour
{
    [SerializeField]
    private GameObject sword;

    [SerializeField]
    private float cooltime;

    private float rotateSpeed;

    private float duration;

    GameObject tempSword;

    void Start()
    {
        cooltime = 3f;

        rotateSpeed = 720f;

        duration = 0.5f;

        tempSword = Instantiate(sword, transform.position + new Vector3(0, 0.2f, 0), transform.rotation);

        tempSword.SetActive(false);
    }

    void Update()
    {
        cooltime -= Time.deltaTime;

        if (cooltime <= 0)
        {
            SweepSword();

            cooltime = 2f;
        }
    }

    void SweepSword()
    {
        tempSword.transform.position = transform.position + new Vector3(0, 0.2f, 0);

        tempSword.SetActive(true);

        tempSword.transform.SetParent(null);

        StartCoroutine(RotateSword(tempSword));
    }

    IEnumerator RotateSword(GameObject swordObj) // TODO: 회전 로직 나중에 다시 수정
    {
        // rotateSpeed * duration = 360 가 되어야 함.

        rotateSpeed = 1440f;

        duration = 0.25f;

        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float step = rotateSpeed * Time.deltaTime;

            swordObj.transform.Rotate(Vector3.up, step);  // Y축을 기준으로 회전

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        tempSword.SetActive(false);
    }
}
