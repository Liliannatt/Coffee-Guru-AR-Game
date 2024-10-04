using UnityEngine;

public class ButtonActions : MonoBehaviour
{
    public GameObject voiceControlObject; // 拖拽包含voiceMovement脚本的对象到这个字段

    // 这个方法将在按钮点击时调用
    public void OnFinishMixingButtonClicked()
    {
        // 获取voiceMovement脚本并设置finish_mixing为true
        voiceMovement vm = voiceControlObject.GetComponent<voiceMovement>();
        if (vm != null)
        {
            vm.finish_mixing = true;
        }
    }
}
