using UnityEngine;

public class Blood : MonoBehaviour
{
    private Color bcolor;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        bcolor = rend.material.color;
    }

    void Update()
    {
        if (bcolor.a > 0 && bcolor.a < 1)
        {
            bcolor.a -= 0.1f * Time.deltaTime;
            rend.material.color = bcolor; // 수정된 알파를 실제 머티리얼에 적용
        }
        Destroy(this.gameObject, 3);
    }
}
