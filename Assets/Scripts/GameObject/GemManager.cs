using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GemManager : MonoBehaviour
{
    private Animator hoverAnimation;

    private float experiencePoint = 1.5f;

    private float moveSpeed = 3f;

    private int playerLayer;

    private void Start()
    {
        hoverAnimation = gameObject.GetComponent<Animator>();

        playerLayer = LayerMask.GetMask("PLAYER");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.LookAt(other.transform);

            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            if (Physics.CheckSphere(transform.position, 0.08f, playerLayer))
            {
                other.GetComponent<IScoreable>()?.GetExp(experiencePoint);

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hoverAnimation.enabled = false;
        }
    }
}
