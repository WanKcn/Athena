using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// ----------     使用拖拽的方式     ----------
// using UnityEngine.Events;
// // 事件 需要一个Vector3使人物知道往某一个世界坐标移动
// [System.Serializable]
// public class EventVector3 : UnityEvent<Vector3>{ }
// public EventVector3 OnMouseClicked;

public class MouseManager : MonoBehaviour
{
    // 单例模式
    public static MouseManager Instance;

    // 创建图片的变量
    public Texture2D point, doorway, attack, target, arrow;

    // 用来保存射线碰撞到物体的相关信息
    RaycastHit hitInfo;

    public event Action<Vector3> OnMouseClicked;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
    }

    void Update()
    {
        // 实时检测射线相关信息，切换鼠标贴图
        SetCursorTexture();
        // 鼠标控制
        MouseControl();
    }


    /// 使鼠标在射线触碰到不同的物体时指针发生变化
    void SetCursorTexture()
    {
        // 制作一条从主摄像机发射出去的射线 返回射线的点是鼠标点击的点
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 使用Physics.Raycast()方法来获取射线碰撞的信息
        if (Physics.Raycast(ray, out hitInfo))
        {
            // 设置鼠标的方式使用系统内置方法 Cursor.SetCursor(图片，hotspot来纪录鼠标点击顶点偏移，自动切换)
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                    break;
            }
        }
    }

    /// 控制鼠标 此时update执行过SetCursorTexture();已经获得hitinfo
    void MouseControl()
    {
        // 需要判断鼠标点击的瞬间返回的信息是什么东西 假如是地面 然后触发鼠标事件event把点击地面的数值的点传回到数值中
        // 点击一下鼠标GetMouseButtonDown 鼠标左键为0，同时需要确定鼠标点击的位置不能为空(如果点击地面以为返回值为空)
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            // 每次点击鼠标在地面上，将点击的点传回到事件 事件把人物的Agent.destination设置到点击的点，实现人物移动
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
            {
                // 如果事件不为空，启动事件传入一个Vector3的值 
                OnMouseClicked?.Invoke(hitInfo.point);
            }
        }
    }
}