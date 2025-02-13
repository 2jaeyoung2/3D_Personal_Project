using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class FacingTarget : MonoBehaviour
{
    bool isRayOn;
    float distance;

    Ray ray;
    public static RaycastHit info;

    private void Update()
    {
        RotateToCursor();
    }

    void RotateToCursor()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.black);

        int floorLayerMask = LayerMask.GetMask("FLOOR");

        if (Physics.Raycast(ray, out info, 50f, floorLayerMask)) // Ray가 무언가에 충돌했는지 확인
        {
            transform.LookAt(info.point);
        }
    }
}