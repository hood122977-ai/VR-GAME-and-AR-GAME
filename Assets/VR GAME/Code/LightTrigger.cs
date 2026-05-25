using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Light pointLight17;
    [SerializeField] private GameObject monterTiger3;
    [SerializeField] private GameObject soundObject;  // ← 추가

    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private AudioClip lightSwitchClip;

    private AudioSource audioSource;
    private bool isTriggered = false;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        pointLight17.enabled = false;
    }

    private void PlayLightSound()
    {
        if (lightSwitchClip != null)
            audioSource.PlayOneShot(lightSwitchClip);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        if (!other.CompareTag(playerTag)) return;

        isTriggered = true;
        pointLight17.enabled = true;
        PlayLightSound();

        if (monterTiger3 != null)
            monterTiger3.SetActive(false);

        if (soundObject != null)
            soundObject.SetActive(false);  // ← 추가
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}