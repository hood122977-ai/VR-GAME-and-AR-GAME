using UnityEngine;
using System.Collections;

public class MonterTiger3Trigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Light pointLight13;
    [SerializeField] private BoxCollider wall2Collider;
    [SerializeField] private LookBackRaycast lookBackRaycast;
    [SerializeField] private AudioClip lightSwitchClip;

    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";

    private bool isTriggered = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        pointLight13.enabled = false;
        wall2Collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        if (!other.CompareTag(playerTag)) return;

        isTriggered = true;
        pointLight13.enabled = true;

        // 소리 루프 재생
        audioSource.clip = lightSwitchClip;
        audioSource.loop = true;
        audioSource.Play();

        wall2Collider.isTrigger = false;
        lookBackRaycast.EnableLookBack();
        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            // 8초 후 2초 꺼짐
            yield return new WaitForSeconds(8f);
            pointLight13.enabled = false;
            yield return new WaitForSeconds(2f);
            pointLight13.enabled = true;

            // 7초 후 2초 꺼짐 (15초 시점)
            yield return new WaitForSeconds(9f);
            pointLight13.enabled = false;
            yield return new WaitForSeconds(2f);
            pointLight13.enabled = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}