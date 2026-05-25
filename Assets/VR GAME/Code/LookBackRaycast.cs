using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookBackRaycast : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Light pointLight16;
    [SerializeField] private GameObject duckMoveObject;
    [SerializeField] private List<Animator> duckAnimators;
    [SerializeField] private GameObject wallObject;
    [SerializeField] private GameObject wall2Object;
    [SerializeField] private AudioClip lightSwitchClip;
    [SerializeField] private AudioClip duckAppearClip;
    [SerializeField] private List<AudioClip> duckLoopClips;

    [Header("Settings")]
    [SerializeField] private float flickerInterval = 0.15f;
    [SerializeField] private float duckRunDelay = 1f;
    [SerializeField] private float duckMoveSpeed = 3f;
    [SerializeField] private string runAnimName = "Run";
    [SerializeField] private LayerMask lookBackLayer;
    [SerializeField] private float duckAppearSoundDelay = 0f;
    [SerializeField] private float loopSoundDelay = 0f;
    [SerializeField] private float loopSoundInterval = 0.5f;

    private bool isEnabled = false;
    private bool isTriggered = false;
    private Coroutine overlappingSoundCoroutine;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        pointLight16.enabled = false;
        duckMoveObject.SetActive(false);
    }

    private void PlayLightSound()
    {
        if (lightSwitchClip != null)
            audioSource.PlayOneShot(lightSwitchClip);
    }

    public void EnableLookBack()
    {
        isEnabled = true;
    }

    private void Update()
    {
        if (!isEnabled || isTriggered) return;

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 10f, lookBackLayer))
        {
            isTriggered = true;
            StartCoroutine(HandleLookBackEvent());
        }
    }

    private IEnumerator HandleLookBackEvent()
    {
        pointLight16.enabled = true;
        PlayLightSound();

        yield return new WaitForSeconds(flickerInterval);
        pointLight16.enabled = false;
        PlayLightSound();
        yield return new WaitForSeconds(flickerInterval);
        pointLight16.enabled = true;
        PlayLightSound();

        yield return new WaitForSeconds(flickerInterval);
        pointLight16.enabled = false;
        PlayLightSound();
        duckMoveObject.SetActive(true);

        StartCoroutine(PlayAppearSound());
        overlappingSoundCoroutine = StartCoroutine(PlayOverlappingSound());

        yield return new WaitForSeconds(flickerInterval);
        pointLight16.enabled = true;
        PlayLightSound();

        yield return new WaitForSeconds(duckRunDelay);

        foreach (Animator anim in duckAnimators)
        {
            if (anim != null)
                anim.Play(runAnimName, 0, 0f);
        }

        wallObject.SetActive(false);
        wall2Object.SetActive(false);
        StartCoroutine(MoveDuckForward());
    }

    private IEnumerator PlayAppearSound()
    {
        yield return new WaitForSeconds(duckAppearSoundDelay);
        if (duckAppearClip != null)
            audioSource.PlayOneShot(duckAppearClip);
    }

    private IEnumerator PlayOverlappingSound()
    {
        yield return new WaitForSeconds(loopSoundDelay);
        while (true)
        {
            foreach (AudioClip clip in duckLoopClips)
            {
                if (clip != null)
                    audioSource.PlayOneShot(clip);
            }
            yield return new WaitForSeconds(loopSoundInterval);
        }
    }

    private IEnumerator MoveDuckForward()
    {
        while (duckMoveObject.activeSelf)
        {
            duckMoveObject.transform.position -= duckMoveObject.transform.forward * duckMoveSpeed * Time.deltaTime;
            yield return null;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 10f);
    }

    public void StopAllSounds()
    {
        audioSource.Stop();
        if (overlappingSoundCoroutine != null)
            StopCoroutine(overlappingSoundCoroutine);
    }
}