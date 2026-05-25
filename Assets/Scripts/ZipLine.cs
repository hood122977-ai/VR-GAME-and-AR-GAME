using UnityEngine;

public class ZipLine : MonoBehaviour
{
    public GameObject Player;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 3f;
    public bool isZip = false;

    public GameObject ziplineObject;

    // Update is called once per frame
    void Update()
    {
        if (isZip)
        {
            Player.transform.position = ziplineObject.transform.position;
            ziplineObject.transform.position =
                Vector3.MoveTowards(ziplineObject.transform.position, endPoint.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.transform.position = startPoint.position;
            isZip = true;
        }
    }

}