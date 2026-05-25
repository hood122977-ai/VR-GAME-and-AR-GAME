using System.Collections;
using UnityEngine;

public class AttakVolume : MonoBehaviour
{
    public GameObject bloodPrefab;
    public Collider attackCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //blood = GameObject.FindGameObjectWithTag("Blood");
        attackCollider = GetComponent<Collider>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            StartCoroutine("ToggleCollider");
            float r = Random.Range(0.0f, 360.0f);
            GameObject blood = Instantiate(bloodPrefab, other.transform);
            blood.transform.localRotation = Quaternion.Euler(0, 0, r);
        }
    }

    IEnumerator ToggleCollider()
    {
        attackCollider.enabled = false;
        yield return new WaitForSeconds(1);
        attackCollider.enabled = true;
    }
}
