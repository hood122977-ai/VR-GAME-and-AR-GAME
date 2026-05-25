using System.Collections;
using UnityEngine;

public class StartSceneSound : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioClip soundClip;

    [Header("Settings")]
    [SerializeField] private float startDelay = 0f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(startDelay);

        if (soundClip != null)
            audioSource.PlayOneShot(soundClip);
    }
}