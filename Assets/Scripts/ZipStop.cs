using System.IO.Compression;
using UnityEngine;

public class ZipStop : MonoBehaviour
{
    public ZipLine zipLineScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            zipLineScript.isZip = false; // ZipLine 스크립트의 isZip을 false로 설정
            zipLineScript.ziplineObject.transform.position = zipLineScript.startPoint.position; // ZipLine 오브젝트를 끝점으로 이동
        }
    }
}
