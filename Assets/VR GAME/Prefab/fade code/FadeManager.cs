using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [Header("References")]
    [SerializeField] private Image fadeImage;

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 1.5f;

    private bool isFading = false;  // ← 중복 페이드 방지

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);  ← 이 줄 제거!
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    public void LoadScene(string sceneName)
    {
        if (isFading) return;  // ← 이미 페이드 중이면 중복 호출 방지
        StartCoroutine(FadeIn(sceneName));
    }

    private IEnumerator FadeOut()
    {
        isFading = true;
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, time / fadeDuration);  // ← Lerp로 부드럽게
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
        isFading = false;
    }

    private IEnumerator FadeIn(string sceneName)
    {
        isFading = true;
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, time / fadeDuration);  // ← Lerp로 부드럽게
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;

        SceneManager.LoadScene(sceneName);

        // 씬 로드 후 한 프레임 대기
        yield return null;
        StartCoroutine(FadeOut());
    }
}