using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间

public class PrintPosRot : MonoBehaviour
{
    public Transform objectToTrack; // 要追踪的物体
    public Text positionText; // 显示位置的UI Text
    public Text rotationText; // 显示旋转的UI Text

    void Update()
    {
        // 更新位置和旋转的Text元素内容
        if (objectToTrack != null && positionText != null && rotationText != null)
        {
            positionText.text = "Position: " + objectToTrack.position.ToString();
            rotationText.text = "Rotation: " + objectToTrack.rotation.ToString();
        }
    }
}