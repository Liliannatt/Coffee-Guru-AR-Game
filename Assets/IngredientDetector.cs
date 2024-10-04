using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Vuforia; // 确保引入Vuforia命名空间

public class IngredientDetector : MonoBehaviour
{
    public Text ingredientCountText; // UI Text组件引用
    private Dictionary<string, int> ingredientCounts = new Dictionary<string, int>(); // 存储每种原料的数量

    public int espressoCount;
    public int milkCount;
    public int milkfoamCount;
    public int waterCount;

    // 参考到 ObjectDetectionManager 组件
    public ObjectDetectionManager objectDetectionManager;

    void Start()
    {
        // 订阅事件
        objectDetectionManager.OnObjectDetected += UpdateIngredientCount;
    }

    void UpdateIngredientCount(string ingredientName)
    {
        // 如果字典中不存在这个物品的记录，就添加它
        if (!ingredientCounts.ContainsKey(ingredientName))
        {
            ingredientCounts[ingredientName] = 0;
        }
        // 更新物品的计数
        ingredientCounts[ingredientName]++;

        if (ingredientName == "QR_espresso")
        {
            espressoCount = ingredientCounts[ingredientName];
        }
        else if (ingredientName == "QR_water")
        {
            waterCount = ingredientCounts[ingredientName];
        }
        else if (ingredientName == "QR_milkfoam")
        {
            milkfoamCount = ingredientCounts[ingredientName];
        }
        else if (ingredientName == "QR_milk")
        {
            milkCount = ingredientCounts[ingredientName];
        }

        // 更新UI
        UpdateIngredientText();
        
    }

    void UpdateIngredientText()
    {
        ingredientCountText.text = "";
        foreach (var ingredient in ingredientCounts)
        {
            ingredientCountText.text += ingredient.Key + ": " + ingredient.Value + "\n";
        }
    }

    void OnDestroy()
    {
        // 取消订阅事件
        
        if (objectDetectionManager != null)
        {
            objectDetectionManager.OnObjectDetected -= UpdateIngredientCount;
        }
        
    }
}
