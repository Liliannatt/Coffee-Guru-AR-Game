using UnityEngine;

public class MixingManager : MonoBehaviour
{
    // 期望的比例
    public Vector4 correctProportions;

    // 可以通过Inspector设置的模型和错误信息的引用
    public GameObject coffeeModel;
    public GameObject errorText;

    // Voice Control对象的引用
    public GameObject voiceControlObject;

    public IngredientDetector ingredientDetector_esp;
    public IngredientDetector ingredientDetector_milk;
    public IngredientDetector ingredientDetector_milkfoam;
    public IngredientDetector ingredientDetector_water;

    private voiceMovement vm;

    public Camera arCamera;
    public int direction;

    void Start()
    {
        vm = voiceControlObject.GetComponent<voiceMovement>();
        if (vm == null)
        {
            Debug.LogError("VoiceMovement component not found on the voice control object!");
        }
    }

    void Update()
    {
        // 检查是否完成混合
        if (vm != null && vm.finish_mixing)
        {
            // 检查原料比例是否正确
            CheckProportions();
            // 重置finish_mixing标志以防止重复检测
            vm.finish_mixing = false;
        }
    }

    void CheckProportions()
    {
        // 检查原料比例是否符合预定的比例
        bool isProportionCorrect =
            ingredientDetector_esp.espressoCount == correctProportions.x &&
            ingredientDetector_milk.milkCount == correctProportions.y &&
            ingredientDetector_milkfoam.milkfoamCount == correctProportions.z &&
            ingredientDetector_water.waterCount == correctProportions.w;

        // 显示咖啡模型或错误信息
        coffeeModel.SetActive(isProportionCorrect);
        errorText.SetActive(!isProportionCorrect);
       
    }

}
