using System.Collections;
using UnityEngine;

public class RoomEventTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Light roomLight;
    [SerializeField] private GameObject monsterObject;
    [SerializeField] private Light keyLight;
    [SerializeField] private GameObject keyWow;
    [SerializeField] private GameObject duck1234555Object;
    [SerializeField] private Animator duck1234555Animator;
    [SerializeField] private GameObject toggleObject;
    [SerializeField] private AudioClip lightSwitchClip;
    [SerializeField] private AudioClip keyLightSwitchClip;
    [SerializeField] private AudioClip keyLightOffClip;
    [SerializeField] private AudioClip duckSoundClip;
    [SerializeField] private AudioClip duck1234555Clip;

    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float flickerInterval = 0.4f;
    [SerializeField] private float keyAppearDelay = 3f;
    [SerializeField] private string willAnimName = "will";
    [SerializeField] private float reactivateDelay = 0.5f;

    private bool isTriggered = false;
    private AudioSource audioSource;
    private Coroutine flickerCoroutine;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        roomLight.enabled = false;
        monsterObject.SetActive(false);
        keyLight.enabled = false;
        keyWow.SetActive(false);
        duck1234555Object.SetActive(false);
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
        StartCoroutine(HandleRoomEvent());
    }

    private IEnumerator HandleRoomEvent()
    {
        // 1번째 깜빡임
        roomLight.enabled = true;
        PlayLightSound();
        yield return new WaitForSeconds(flickerInterval);
        roomLight.enabled = false;
        PlayLightSound();
        yield return new WaitForSeconds(flickerInterval);

        // 2번째 깜빡임 + 몬스터 등장
        roomLight.enabled = true;
        PlayLightSound();
        monsterObject.SetActive(true);
        audioSource.PlayOneShot(duckSoundClip);
        yield return new WaitForSeconds(flickerInterval);
        roomLight.enabled = false;
        PlayLightSound();
        monsterObject.SetActive(false);
        yield return new WaitForSeconds(flickerInterval);

        // 3번째 깜빡임
        roomLight.enabled = true;
        PlayLightSound();
        yield return new WaitForSeconds(flickerInterval);

        // 라이트 꺼짐
        roomLight.enabled = false;
        PlayLightSound();

        // keyAppearDelay 후 keyLight 켜짐 + Key wow 등장
        yield return new WaitForSeconds(keyAppearDelay);
        keyLight.enabled = true;
        audioSource.clip = keyLightSwitchClip;
        audioSource.loop = true;
        audioSource.Play();
        keyWow.SetActive(true);

        flickerCoroutine = StartCoroutine(FlickerLight(keyLight));

        // Key wow 집을 때까지 대기
        yield return new WaitUntil(() => IsKeyGrabbed());

        // flickerCoroutine 멈춤 + keyLight 꺼짐
        if (flickerCoroutine != null)
            StopCoroutine(flickerCoroutine);
        keyLight.enabled = false;
        audioSource.Stop();
        if (keyLightOffClip != null)
            audioSource.PlayOneShot(keyLightOffClip);
        if (toggleObject != null)
            toggleObject.SetActive(false);

        // 2초 후 Duck1234555 등장 + 소리
        yield return new WaitForSeconds(2f);
        duck1234555Object.SetActive(true);
        if (duck1234555Clip != null)
            audioSource.PlayOneShot(duck1234555Clip);
        duck1234555Animator.Play(willAnimName, 0, 0f);

        yield return null;
        float clipLength = duck1234555Animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(clipLength);
        duck1234555Object.SetActive(false);

        yield return new WaitForSeconds(reactivateDelay);
        if (toggleObject != null)
            toggleObject.SetActive(true);
    }

    private IEnumerator FlickerLight(Light light)
    {
        while (true)
        {
            yield return new WaitForSeconds(8f);
            light.enabled = false;
            yield return new WaitForSeconds(2f);
            light.enabled = true;

            yield return new WaitForSeconds(9f);
            light.enabled = false;
            yield return new WaitForSeconds(2f);
            light.enabled = true;
        }
    }

    private bool IsKeyGrabbed()
    {
        var grab = keyWow.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grab == null) return false;
        return grab.isSelected;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}