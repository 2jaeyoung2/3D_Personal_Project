﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Rigidbody rb;

    private Vector3 moveDir;

    private Vector3 tempPosition;

    [SerializeField]
    private Animator warriorAnim;

    [SerializeField]
    private Animator magicianAnim;

    [SerializeField]
    private Animator bomberAnim;

    private void Start()
    {
        moveSpeed = 3f;
    }

    private void FixedUpdate()
    {
        if (moveDir != Vector3.zero) // 키 입력이 있으면 실행.
        {
            warriorAnim.SetBool("isMoving", true);

            magicianAnim.SetBool("isMoving", true);

            bomberAnim.SetBool("isMoving", true);

            // TODO: 끊기는거 수정
            if (gameObject.transform.position.x <= 10 && gameObject.transform.position.x >= -10 && gameObject.transform.position.z <= 10 && gameObject.transform.position.z >= -10)
            {
                Vector3 move = new Vector3(moveDir.x, 0, moveDir.z) * moveSpeed; // transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);

                rb.velocity = new Vector3(move.x, 0, move.z);

                tempPosition = gameObject.transform.position;
            }
            else
            {
                gameObject.transform.position = tempPosition;
            }
        }
        else
        {
            warriorAnim.SetBool("isMoving", false);

            magicianAnim.SetBool("isMoving", false);

            bomberAnim.SetBool("isMoving", false);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            Vector2 direction = ctx.ReadValue<Vector2>();

            moveDir = new Vector3(direction.x, 0, direction.y);
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            moveDir = Vector3.zero;

            rb.velocity = Vector3.zero;

        }
    }
}