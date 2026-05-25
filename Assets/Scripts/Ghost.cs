using UnityEngine;

public class Ghost : MonoBehaviour
{
    Transform target;
    private float followSpeed = 0.5f;
    Animator animator;
    bool isChase = false;
    public GameObject attackVolume;

    bool isDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = Camera.main.transform;
        animator = GetComponent<Animator>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)//죽음
        {
            attackVolume.SetActive(false);
            return; //아무것도 안함
        }

        else if (!isDead) //살아있음
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (isChase)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);
                transform.LookAt(target);
            }



            else if (distance < 5 && distance > 1)
            {
                animator.SetBool("isMove", true);
                animator.SetBool("isAttack", false);
                isChase = true;
            }

            else if (distance < 1) //공격
            {
                animator.SetBool("isAttack", true);
                isChase = false;
                attackVolume.SetActive(true);
            }

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("bullet"))
        {
            animator.SetTrigger("isDead");
            //animator.enabled = false; //1.애드포스하려면 애니메이션 종료 필요
            GetComponent<Rigidbody>().useGravity = true; //트루로해야함
            isChase = false;
            //GetComponent<Rigidbody>().AddForce((transform.position - collision.contacts[0].point).normalized *30f, ForceMode.Impulse); //2.애드포스하려 애니메이션종료필요
            isDead = true;
            Destroy(gameObject, 3);
        }
    }
}
