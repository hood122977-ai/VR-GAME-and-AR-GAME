using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationHandOnInput : MonoBehaviour
{
    // 트리거 입력 값 (손가락 집는 동작, 검지)
    public InputActionProperty pinchAnimationAction;
    // 그립 입력 값 (손 쥐는 동작, 나머지 손가락)
    public InputActionProperty gripAnimationAction;
    // 손 애니메이터 (Animator 컴포넌트)
    public Animator handAnimator;
    // 매 프레임마다 실행되는 함수
    void Update()
    {
        // 트리거 입력 값을 읽어옴 (0 ~ 1 사이 값)
        // 0 = 안 누름, 1 = 끝까지 누름
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        // Animator의 "Trigger" 파라미터 값으로 전달
        // → 손가락(검지) 애니메이션이 입력 값에 따라 움직임
        handAnimator.SetFloat("Trigger", triggerValue);
        // 그립 입력 값을 읽어옴 (0 ~ 1 사이 값)
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        // Animator의 "Grip" 파라미터 값으로 전달
        // → 손 전체를 쥐는 애니메이션이 입력 값에 따라 움직임
        handAnimator.SetFloat("Grip", gripValue);
    }
}