using UnityEngine;

public class OpenTiger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioClip doorOpenClip;  // ¡ç Ãß°¡

    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";
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
        if (!other.CompareTag(playerTag)) return;

        isTriggered = true;
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