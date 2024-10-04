using UnityEngine;

public class KeepInCenter : MonoBehaviour
{
    public Camera arCamera; // 指定AR摄像机
    public float distanceFromCamera = 2.0f; // 指定距摄像机的距离

    void Update()
    {
        if (arCamera != null)
        {
            // 设置模型位置为摄像机前方的中心点
            Vector3 newPosition = arCamera.transform.position + arCamera.transform.forward * distanceFromCamera;
            transform.position = newPosition;

            // 可选: 确保模型始终面向摄像机
            transform.LookAt(arCamera.transform);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
        else
        {
            Debug.LogError("AR Camera is not assigned.");
        }
    }
}
