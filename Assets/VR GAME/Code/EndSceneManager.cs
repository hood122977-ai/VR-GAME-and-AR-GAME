using System.Collections;
using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioClip endClip;
    [SerializeField] private float startDelay = 0f; // 소리 나오는 타이밍 (초)
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void Start()
    {
        if (endClip != null)
            StartCoroutine(PlayWithDelay());
    }

    private IEnumerator PlayWithDelay()
    {
        yield return new WaitForSeconds(startDelay);
        audioSource.clip = endClip;
        audioSource.Play();
    }
}