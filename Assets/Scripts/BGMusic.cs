using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BGMusic : MonoBehaviour
{
    public static BGMusic instance;
    private AudioSource audioSource;
    public float fadeDuration = 1f;

    [Tooltip("Optional: List scene names to disable this music in.")]
    public string[] disableInScenes;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (var s in disableInScenes)
        {
            if (scene.name == s)
            {
                StartCoroutine(FadeOutAndDestroy());
                return;
            }
        }
    }

    public IEnumerator FadeOutAndDestroy()
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        Destroy(gameObject);
    }

    public IEnumerator FadeOut(float targetVolume = 0f)
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / fadeDuration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    public IEnumerator FadeIn(AudioClip newClip, float targetVolume = 1f)
    {
        if (audioSource == null)
            yield break;

        // Make sure the AudioSource is enabled before using it
        if (!audioSource.enabled)
            audioSource.enabled = true;

        if (audioSource.isPlaying)
            yield return StartCoroutine(FadeOut(0f));

        audioSource.clip = newClip;

        // Critical: Re-check here in case clip is null
        if (newClip != null)
        {
            audioSource.Play();

            float time = 0f;
            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(0f, targetVolume, time / fadeDuration);
                yield return null;
            }

            audioSource.volume = targetVolume;
        }
    }

    public void PlayNewMusic(AudioClip newClip, float volume = 1f)
    {
        if (newClip == null || audioSource == null)
            return;

        StartCoroutine(FadeIn(newClip, volume));
    }
}
