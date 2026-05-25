using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleTypewriter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI titleText;

    [Header("Settings")]
    [SerializeField] private string fullText = "DARK VR GAME";
    [SerializeField] private float charInterval = 1.5f;
    [SerializeField] private float delayAfterComplete = 1f;
    [SerializeField] private string nextSceneName = "Game Scene";

    private void Start()
    {
        titleText.text = "";
        StartCoroutine(TypewriterEffect());
    }

    private IEnumerator TypewriterEffect()
    {
        // 글자 하나씩 추가
        foreach (char c in fullText)
        {
            titleText.text += c;
            yield return new WaitForSeconds(charInterval);
        }

        // 다 적히면 잠깐 대기 후 씬 이동
        yield return new WaitForSeconds(delayAfterComplete);
        SceneManager.LoadScene(nextSceneName);
    }
}