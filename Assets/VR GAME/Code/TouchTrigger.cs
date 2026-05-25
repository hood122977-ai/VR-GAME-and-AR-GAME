using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TouchTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator cubeAnimator;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private List<Light> lightsToToggle;
    [SerializeField] private List<GameObject> objectsToEnable;
    [SerializeField] private List<GameObject> objectsToDisable;
    [SerializeField] private AudioClip lightSwitchClip;
    [SerializeField] private AudioClip clickAnimClip;  // ← 추가

    [Header("Settings")]
    [SerializeField] private string cubeAnimParam = "tiger";
    [SerializeField] private string doorAnimParam = "tiger";
    [SerializeField] private float lightDelay = 1f;

    private bool isTriggered = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        var interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null)
            interactable.selectEntered.AddListener(OnClick);
    }

    private void PlayLightSound()
    {
        if (lightSwitchClip != null)
            audioSource.PlayOneShot(lightSwitchClip);
    }

    private void OnClick(SelectEnterEventArgs args)
    {
        if (isTriggered) return;
        isTriggered = true;

        cubeAnimator.SetTrigger(cubeAnimParam);
        doorAnimator.SetTrigger(doorAnimParam);

        // 클릭 애니메이션과 함께 소리 재생
        if (clickAnimClip != null)
            audioSource.PlayOneShot(clickAnimClip);

        StartCoroutine(HandleLightAndObjects());
    }

    private IEnumerator HandleLightAndObjects()
    {
        foreach (Light light in lightsToToggle)
        {
            if (light != null)
                light.enabled = false;
        }
        PlayLightSound();

        yield return new WaitForSeconds(lightDelay);

        foreach (Light light in lightsToToggle)
        {
            if (light != null)
                light.enabled = true;
        }
        PlayLightSound();

        foreach (GameObject obj in objectsToEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        foreach (GameObject obj in objectsToDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        var interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnClick);
    }
}