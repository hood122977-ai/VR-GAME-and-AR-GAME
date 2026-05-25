using UnityEngine;

public class MonsterDisappear : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject monsterObject;

    [Header("Settings")]
    [SerializeField] private string monsterTag = "Monster";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(monsterTag)) return;

        monsterObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}