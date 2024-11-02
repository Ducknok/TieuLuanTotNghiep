using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : DucMonobehavior
{
    public DamagePopup instance;
    protected override void Start()
    {
        //this.instance.Create(Vector3.zero, 300f);
    }
    protected override void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Chuyển đổi sang tọa độ thế giới
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        // Đặt z = 0 nếu bạn chỉ dùng trong không gian 2D
        mouseWorldPosition.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            bool isCriticalHit = Random.Range(0, 100) < 30;
           this.instance.Create(mouseWorldPosition, 100f, isCriticalHit);
        }
    }
}
