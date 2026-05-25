using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTiger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Light pointLight24;
    [SerializeField] private GameObject duckClone;
    [SerializeField] private GameObject duckEnt;
    [SerializeField] private Animator duckEntAnimator;
    [SerializeField] private Transform cameraOffset;
    [SerializeField] private AudioClip lightSwitchClip;
    [SerializeField] private AudioClip duckEntClip;
    [SerializeField] private GameObject soundObject;

    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float flickerInterval = 0.8f;
    [SerializeField] private string endSceneName = "End Scene";
    [SerializeField] private string duckEntAnimParam = "doOne";
    [SerializeField] private float endDelay = 2f;

    [Header("Camera Shake")]
    [SerializeField] private float shakeIntensity = 0.05f;
    [SerializeField] private float shakeDuration = 5f;
    [SerializeField] private float zoomAmount = 1f;  // ← 추가



    private bool isTriggered = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        pointLight24.enabled = false;
        duckClone.SetActive(false);
        duckEnt.SetActive(false);
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

        if (soundObject != null)
            soundObject.SetActive(false);

        StartCoroutine(HandleEndEvent());
    }

    private IEnumerator HandleEndEvent()
    {
        // 1번째 깜빡임
        pointLight24.enabled = true;
        PlayLightSound();
        yield return new WaitForSeconds(flickerInterval);
        pointLight24.enabled = false;
        PlayLightSound();
        yield return new WaitForSeconds(flickerInterval);

        // 2번째 깜빡임 + Duck(Clone) 활성화
        pointLight24.enabled = true;
        PlayLightSound();
        duckClone.SetActive(true);
        yield return new WaitForSeconds(flickerInterval);
        pointLight24.enabled = false;
        PlayLightSound();
        duckClone.SetActive(false);
        yield return new WaitForSeconds(flickerInterval);

        // 3번째 깜빡임 + Duck Ent 활성화
        pointLight24.enabled = true;
        PlayLightSound();
        duckEnt.SetActive(true);
        audioSource.PlayOneShot(duckEntClip);
        duckEntAnimator.SetTrigger(duckEntAnimParam);

        StartCoroutine(Shake());

        // 점점 빠르게 깜빡임
        float interval = flickerInterval;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(interval);
            pointLight24.enabled = !pointLight24.enabled;
            PlayLightSound();
            interval = Mathf.Max(0.05f, interval * 0.8f);
        }
        pointLight24.enabled = true;

        yield return new WaitForSeconds(endDelay);
        SceneManager.LoadScene(endSceneName);
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;
        Vector3 originalPos = cameraOffset.localPosition;

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / shakeDuration;

            // Z축으로 확대 (앞으로 이동)
            float zoomZ = Mathf.Lerp(0f, zoomAmount, t);

            // 흔들림
            float currentShake = shakeIntensity * t;
            cameraOffset.localPosition = originalPos + new Vector3(0f, 0f, zoomZ) + Random.insideUnitSphere * currentShake;

            yield return null;
        }

        cameraOffset.localPosition = originalPos;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}