using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    // 아날로그 값 입력 (예: 트리거, 조이스틱)
    public InputActionProperty testActionValue;

    // 버튼 입력 (예: A 버튼, Trigger 클릭 등)
    public InputActionProperty testActionButton;

    // 매 프레임마다 실행
    void Update()
    {
        // float 타입 값 읽기 (0 ~ 1 사이 값, 트리거 압력 등)
        float value = testActionValue.action.ReadValue<float>();

        // 콘솔에 값 출력
        Debug.Log("Value : " + value);

        // 버튼이 눌렸는지 여부 (true / false)
        bool button = testActionButton.action.IsPressed();

        // 콘솔에 버튼 상태 출력
        Debug.Log("Button : " + button);
    }
}