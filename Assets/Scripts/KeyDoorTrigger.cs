using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class KeyDoorTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject keyObject;
    [SerializeField] private AudioClip doorOpenClip;  // ¡ç Ãß°¡

    [Header("Settings")]
    [SerializeField] private string keyTag = "Key";
    [SerializeField] private string openAnimParam = "doOpen";

    private bool isTriggered = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        if (!other.CompareTag(keyTag)) return;

        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if (grab == null || !grab.isSelected) return;

        isTriggered = true;
        keyObject.SetActive(false);
        doorAnimator.SetTrigger(openAnimParam);

        if (doorOpenClip != null)
            audioSource.PlayOneShot(doorOpenClip);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}