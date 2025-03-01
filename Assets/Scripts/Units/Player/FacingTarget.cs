using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FacingTarget : MonoBehaviour
{
    Ray ray;

    public static RaycastHit info;

    private void FixedUpdate()
    {
        RotateToCursor();
    }

    void RotateToCursor()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.black);

        int floorLayerMask = LayerMask.GetMask("FLOOR");

        if (Physics.Raycast(ray, out info, 50f, floorLayerMask)) // Ray�� ���𰡿� �浹�ߴ��� Ȯ��
        {
            transform.LookAt(info.point);
        }
    }
}