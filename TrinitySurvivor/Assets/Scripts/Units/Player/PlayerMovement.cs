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

    Vector3 moveDir;

    private void Start()
    {
        moveSpeed = 3f;
    }

    private void FixedUpdate()
    {
        if (moveDir != Vector3.zero) // 키 입력이 있으면 실행.
        {
            // transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
            Vector3 move = new Vector3(moveDir.x, 0, moveDir.z) * moveSpeed;
            rb.velocity = new Vector3(move.x, 0, move.z);
        }
    }

    //void OnMove(InputValue inputValue) // 키보드랑 바인딩 된 함수
    //{
    //    Vector2 direction = inputValue.Get<Vector2>();
    //    moveDir = new Vector3(direction.x, 0, direction.y);
    //    Debug.Log("Move Direction: " + moveDir);
    //}

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