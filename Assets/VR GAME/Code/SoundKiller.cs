using UnityEngine;

public class SoundKiller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LookBackRaycast lookBackRaycast;

    [Header("Settings")]
    [SerializeField] private string targetTag = "Monster";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag)) return;

        lookBackRaycast.StopAllSounds();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}