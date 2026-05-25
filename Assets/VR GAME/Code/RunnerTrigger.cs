using System.Collections;
using UnityEngine;

public class RunnerTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject runnerObject;
    [SerializeField] private Animator runnerAnimator;
    [SerializeField] private AudioClip runnerClip;

    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string runAnimName = "Run";
    [SerializeField] private float moveSpeed = 3f;

    private bool isTriggered = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        runnerObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        if (!other.CompareTag(playerTag)) return;

        isTriggered = true;
        StartCoroutine(ActivateRunner());
    }

    private IEnumerator ActivateRunner()
    {
        runnerObject.SetActive(true);

        // 소리 재생
        if (runnerClip != null)
        {
            audioSource.PlayOneShot(runnerClip);
            yield return new WaitForSeconds(runnerClip.length);
        }

        // 소리 끝난 후 애니메이션 + 이동
        runnerAnimator.Play(runAnimName, 0, 0f);
        StartCoroutine(MoveRunner());
    }

    private IEnumerator MoveRunner()
    {
        while (runnerObject.activeSelf)
        {
            runnerObject.transform.position -= runnerObject.transform.forward * moveSpeed * Time.deltaTime;
            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}