using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class KeyInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject killObject;
    [SerializeField] private Light pointLight;
    [SerializeField] private AudioClip lightSwitchClip;

    [Header("Settings")]
    [SerializeField] private float flickerDelay = 3f;
    [SerializeField] private float flickerInterval = 0.1f;

    private XRGrabInteractable grabInteractable;
    private AudioSource audioSource;
    private bool isGrabbed = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnKeyGrabbed);
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        pointLight.enabled = false;
    }

    private void PlayLightSound()
    {
        if (lightSwitchClip != null)
            audioSource.PlayOneShot(lightSwitchClip);
    }

    private void OnKeyGrabbed(SelectEnterEventArgs args)
    {
        if (isGrabbed) return;
        isGrabbed = true;

        killObject.SetActive(false);
        pointLight.enabled = true;
        PlayLightSound();

        StartCoroutine(FlickerAfterDelay());
    }

    private IEnumerator FlickerAfterDelay()
    {
        yield return new WaitForSeconds(flickerDelay);

        for (int i = 0; i < 2; i++)
        {
            pointLight.enabled = false;
            PlayLightSound();
            yield return new WaitForSeconds(flickerInterval);
            pointLight.enabled = true;
            PlayLightSound();
            yield return new WaitForSeconds(flickerInterval);
        }
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnKeyGrabbed);
    }
}