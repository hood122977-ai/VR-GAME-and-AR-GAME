using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioClip wowClip;
    [SerializeField] private AudioClip bgmClip;

    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";

    private AudioSource audioSource;
    private bool isTriggered = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        if (!other.CompareTag(playerTag)) return;

        isTriggered = true;
        StartCoroutine(PlaySounds());
    }

    private System.Collections.IEnumerator PlaySounds()
    {
        // Wow 사운드 1회 재생
        audioSource.PlayOneShot(wowClip);
        // 변경
        yield return new WaitForSeconds(wowClip.length + 1f);

        // 배경음 루프 재생
        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}