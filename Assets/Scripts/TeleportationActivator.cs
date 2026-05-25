using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TeleportationActivator : MonoBehaviour
{
    // 텔레포트용 레이 (순간이동할 때 사용하는 레이)
    public XRRayInteractor teleportInteractor;

    // 일반 레이 (UI 클릭, 오브젝트 선택 등에 사용)
    public XRRayInteractor rayInteractor;

    // 텔레포트를 활성화할 입력 (버튼, 트리거 등)
    public InputActionProperty teleportActivatorAction;

    // 게임 시작 시 1번 실행
    void Start()
    {
        // 시작할 때 텔레포트 레이를 꺼둠 (보이지 않게)
        teleportInteractor.gameObject.SetActive(false);

        // 버튼이 눌렸을 때 실행될 이벤트 등록
        teleportActivatorAction.action.performed += Action_performed;

        // UI에 마우스(레이)가 올라가면 텔레포트 레이를 꺼버림
        rayInteractor.uiHoverEntered.AddListener(x => DisableTeleportRay());
    }

    // 버튼이 눌렸을 때 실행되는 함수
    private void Action_performed(InputAction.CallbackContext obj)
    {
        // 현재 일반 레이가 UI 위에 있다면
        if (rayInteractor && rayInteractor.IsOverUIGameObject())
        {
            // 텔레포트 실행 안함 (UI 클릭 우선)
            return;
        }

        // 텔레포트 레이 활성화 (보이게 + 사용 가능)
        teleportInteractor.gameObject.SetActive(true);
    }

    // 텔레포트 레이를 끄는 함수
    public void DisableTeleportRay()
    {
        teleportInteractor.gameObject.SetActive(false);
    }

    // 매 프레임 실행 (게임이 돌아가는 동안 계속)
    void Update()
    {
        // 버튼을 "뗐을 때"
        if (teleportActivatorAction.action.WasReleasedThisFrame())
        {
            // 텔레포트 레이 비활성화 (숨김)
            teleportInteractor.gameObject.SetActive(false);
        }
    }
}