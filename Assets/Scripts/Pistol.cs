using UnityEngine;

public class Pistol : MonoBehaviour
{
    // 발사할 총알 프리팹 (미리 만들어둔 총알 오브젝트)
    public GameObject bulletPrefab;

    // 총알이 발사되는 위치와 방향 (총구 위치)
    public Transform firePoint;

    // 총알의 속도
    public float bulletSpeed = 20f;

    // 총알이 몇 초 뒤에 사라질지
    public float bulletLifetime = 5f;

    // 발사 사운드 클립
    public AudioClip clip;

    // AudioSource 컴포넌트 (소리를 재생하는 역할)
    private AudioSource source;

    // 게임 시작 시 한 번 실행
    void Start()
    {
        // 현재 오브젝트에 붙어있는 AudioSource 가져오기
        source = GetComponent<AudioSource>();
    }

    // 총알 발사 함수 (외부에서 호출됨, 예: 트리거 눌렀을 때)
    public void fireBullet()
    {
        // firePoint 위치와 방향으로 총알 생성
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 생성된 총알의 Rigidbody 가져오기
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // 총 발사 소리 재생
        source.PlayOneShot(clip);

        // Rigidbody가 존재할 경우에만 실행
        if (rb != null)
        {
            // 총알을 firePoint의 앞 방향으로 속도를 주어 발사
            // (Unity 최신 버전에서는 linearVelocity 사용)
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        // 일정 시간 후 총알 삭제 (메모리 관리 + 성능)
        Destroy(bullet, bulletLifetime);
    }
}