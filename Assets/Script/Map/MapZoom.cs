using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapZoom : DucMonobehavior
{
    [SerializeField] private RectTransform mapRectTransform; // RectTransform của bản đồ
    [SerializeField] private float zoomSpeed = 0.1f;        // Tốc độ zoom
    [SerializeField] private float minZoom = 1.0f;          // Mức zoom nhỏ nhất
    [SerializeField] private float maxZoom = 3.0f;          // Mức zoom lớn nhất

    protected override void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            // Lấy tỷ lệ scale hiện tại và tính toán tỷ lệ scale mới
            float newScale = Mathf.Clamp(mapRectTransform.localScale.x + scroll * zoomSpeed, minZoom, maxZoom);

            // Đảm bảo tỷ lệ mới không vượt quá giới hạn
            mapRectTransform.localScale = new Vector3(newScale, newScale, 1f);
        }
    }
}
