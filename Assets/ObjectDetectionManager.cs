using System;
using UnityEngine;
using Vuforia;

public class ObjectDetectionManager : MonoBehaviour
{
    public RectTransform uiImageFrame; // UI框的RectTransform
    public Camera arCamera; // ARCamera引用
    public ObserverBehaviour observerBehaviour; // 物体的Observer组件

    private float detectionTimeThreshold = 3.0f; // 检测时间阈值
    private float timeInsideFrame = 0.0f; // 物体在框内的时间
    private bool isInsideFrame = false; // 物体是否在框内

    public event Action<string> OnObjectDetected;

    void Update()
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(arCamera, observerBehaviour.transform.position);
        Vector3[] corners = new Vector3[4];
        uiImageFrame.GetWorldCorners(corners);
        for (int i = 0; i < corners.Length; i++)
        {
            corners[i] = RectTransformUtility.WorldToScreenPoint(arCamera, corners[i]);
        }

        if (IsWithinBounds(screenPoint, corners))
        {
            if (!isInsideFrame)
            {
                isInsideFrame = true;
                timeInsideFrame = 0.0f;
            }

            timeInsideFrame += Time.deltaTime;

            if (timeInsideFrame >= detectionTimeThreshold)
            {
                OnObjectDetected?.Invoke(observerBehaviour.TargetName);
                observerBehaviour.enabled = false; // 禁用Observer组件
                isInsideFrame = false; // 重置标志
            }
        }
        else
        {
            if (isInsideFrame)
            {
                // 物体离开UI框，准备重新启用检测
                isInsideFrame = false;
                observerBehaviour.enabled = true; // 重新启用Observer组件
            }
            timeInsideFrame = 0.0f;
        }
    }


    private bool IsWithinBounds(Vector2 screenPoint, Vector3[] corners)
    {
        float minX = Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
        float maxX = Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
        float minY = Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
        float maxY = Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y);

        return screenPoint.x >= minX && screenPoint.x <= maxX && screenPoint.y >= minY && screenPoint.y <= maxY;
    }

    // 重新启用检测和显示模型
    public void EnableDetection()
    {
        observerBehaviour.enabled = true;
        isInsideFrame = false;
        timeInsideFrame = 0.0f;
    }
}
