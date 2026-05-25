using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PickupSound : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioClip pickupClip;

    private XRGrabInteractable grabInteractable;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnPickup);
    }

    private void OnPickup(SelectEnterEventArgs args)
    {
        if (pickupClip != null)
            audioSource.PlayOneShot(pickupClip);
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnPickup);
    }
}