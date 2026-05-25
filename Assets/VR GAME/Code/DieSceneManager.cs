using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieSceneManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float waitTime = 5f;
    [SerializeField] private string gameSceneName = "Game Scene";

    [Header("Sound")]
    [SerializeField] private AudioClip lightSwitchClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        PlayLightSound(); // 씬 시작하자마자 소리 재생
        StartCoroutine(ReturnToGame());
    }

    private void PlayLightSound()
    {
        if (lightSwitchClip != null)
            audioSource.PlayOneShot(lightSwitchClip);
    }

    private IEnumerator ReturnToGame()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }
}