using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<Animator> diveAnimators;
    [SerializeField] private AudioClip diveClip;

    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string diveAnimName = "Dive";
    [SerializeField] private float interval = 0.7f;

    private bool isTriggered = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        if (!other.CompareTag(playerTag)) return;

        isTriggered = true;
        StartCoroutine(PlayDiveSequence());
    }

    private IEnumerator PlayDiveSequence()
    {
        while (true)
        {
            foreach (Animator anim in diveAnimators)
            {
                if (anim == null) continue;

                anim.Play(diveAnimName, 0, 0f);

                if (diveClip != null)
                    audioSource.PlayOneShot(diveClip);

                yield return new WaitForSeconds(interval);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}