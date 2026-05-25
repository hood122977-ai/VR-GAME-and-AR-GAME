using UnityEngine;

public class StartSoude : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioClip soundClip;
    private AudioSource audioSource;
    private bool hasPlayed = false; // 한 번만 재생

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            if (soundClip != null)
                audioSource.PlayOneShot(soundClip);

            hasPlayed = true; // 다시 안 울림
        }
    }
}