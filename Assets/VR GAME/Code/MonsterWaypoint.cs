using System.Collections;
using UnityEngine;

public class MonsterWaypoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject monsterObject;

    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float moveSpeed = 2f;

    private bool isTriggered = false;

    private void Start()
    {
        monsterObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        if (!other.CompareTag(playerTag)) return;

        isTriggered = true;
        StartCoroutine(MoveOnX());
    }

    private IEnumerator MoveOnX()
    {
        monsterObject.SetActive(true);

        while (monsterObject.activeSelf)
        {
            Vector3 pos = monsterObject.transform.position;
            pos.x = pos.x + moveSpeed * Time.deltaTime;
            monsterObject.transform.position = pos;

            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}