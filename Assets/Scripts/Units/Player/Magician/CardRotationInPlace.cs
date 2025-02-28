using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRotationInPlace : MonoBehaviour
{
    [SerializeField]
    [Range(100f, 1000f)]
    private float rotateSpeed;

    private void Start()
    {
        rotateSpeed = 800f;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}
