using UnityEngine;
using System.Collections;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript instance;
    [SerializeField] public AudioSource chiptuneBackground; // Credits: Jamez Dahl Music (https://www.youtube.com/watch?v=BXgY7gBiU0E)
    [SerializeField] public AudioSource bounce;
    [SerializeField] public AudioSource hitPlanet;
    [SerializeField] public AudioSource lockIn;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EventManagerScript found.");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        Play(chiptuneBackground);
    }

    public void Play(AudioSource audio)
    {
        audio.volume = 1;
        audio.Play();
    }

    public void Stop(AudioSource audio)
    {
        audio.Stop();
    }

    public void FadeOut(AudioSource audio, float fadeDuration)
    {
        StartCoroutine(FadeOutCoroutine(audio, fadeDuration));
    }

    public void FadeIn(AudioSource audio, float fadeDuration)
    {
        StartCoroutine(FadeInCoroutine(audio, fadeDuration));
    }

    private IEnumerator FadeInCoroutine(AudioSource audioSource, float fadeDuration, float targetVolume = 1f)
    {
        if (audioSource == null) yield break;

        if (fadeDuration <= 0)
        {
            audioSource.volume = targetVolume;
            yield break;
        }

        audioSource.Play();
        audioSource.volume = 0;
        float time = 0;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            time += Time.unscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(0, targetVolume, t * t);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private IEnumerator FadeOutCoroutine(AudioSource audioSource, float fadeDuration)
    {
        float startVolume = audioSource.volume;

        float time = 0;
        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0, t * t);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop(); 
    }
}