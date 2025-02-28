using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{
    private float moveSpeed;

    private GameObject player;

    void Start()
    {
        moveSpeed = 5f;
    }

    private void OnEnable()
    {
        StartCoroutine(ShieldMoveForward());
    }

    IEnumerator ShieldMoveForward()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        gameObject.transform.rotation = player.transform.rotation;

        while (Vector3.Distance(player.transform.position, gameObject.transform.position) < 6f)
        {
            gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            yield return null;
        }

        StartCoroutine(ShieldMoveBackward());
    }

    IEnumerator ShieldMoveBackward()
    {
        while (Vector3.Distance(player.transform.position, gameObject.transform.position) > 0.2f)
        {
            gameObject.transform.LookAt(player.transform);

            gameObject.transform.Translate(Vector3.forward * moveSpeed * 1.5f * Time.deltaTime);

            yield return null;
        }

        gameObject.SetActive(false);

        gameObject.transform.position = player.transform.position + new Vector3(0, 0.2f, 0.3f);
    }
}
