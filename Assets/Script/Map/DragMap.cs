using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMap : DucMonobehavior
{
    [SerializeField] private Transform mapTransform;   // Transform của bản đồ
    [SerializeField] private Camera mapCamera;         // Camera chiếu vào bản đồ (camera quan sát bản đồ)
    private Vector3 lastMousePosition;

    protected override void Update()
    {
        // Khi nhấn chuột trái lần đầu, lưu lại vị trí chuột
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = mapCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        // Khi giữ chuột trái, tính toán khoảng cách di chuyển của chuột và di chuyển bản đồ theo khoảng cách đó
        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = mapCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 delta = currentMousePosition - lastMousePosition;

            // Di chuyển bản đồ bằng cách trừ đi delta (đi ngược chiều di chuyển của chuột)
            mapTransform.position -= new Vector3(delta.x, delta.y, 0);

            // Cập nhật lại vị trí chuột
            lastMousePosition = currentMousePosition;
        }
    }
}
