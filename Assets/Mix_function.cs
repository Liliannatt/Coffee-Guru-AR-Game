using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mix_function : MonoBehaviour
{
    public GameObject objectA; // A物品
    public GameObject objectB; // B物品
    public GameObject objectC; // 将要显示的C物品

    public float thresholdDistance = 0.02f; // A和B物品之间的最小距离

    private bool isObjectCActive = false; // C物品当前的激活状态

    public Text distanceText; // 用于显示距离的UI Text

    void Update()
    {
        // 检查A和B物品是否都被识别了（即它们都处于激活状态）
        if (objectA.activeInHierarchy && objectB.activeInHierarchy)
        {
            float distance = Vector3.Distance(objectA.transform.position, objectB.transform.position);

            if (distanceText != null)
            {
                distanceText.text = "Distance: " + distance.ToString("F2"); // 保留两位小数
            }

            // 如果A和B物品的距离小于阈值，且C物品还未显示，则显示C物品
            if (distance < thresholdDistance && !isObjectCActive)
            {
                Vector3 middlePoint = (objectA.transform.position + objectB.transform.position) / 2;
                objectC.transform.position = middlePoint; // 设置C物品的位置为A和B的中点
                objectC.SetActive(true);
                isObjectCActive = true;
            }
            // 如果A和B物品的距离大于阈值，且C物品已显示，则隐藏C物品
            else if (distance >= thresholdDistance && isObjectCActive)
            {
                objectC.SetActive(false);
                isObjectCActive = false;
            }
        }
    }
}
