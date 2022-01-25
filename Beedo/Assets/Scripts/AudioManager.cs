using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private float fadeDuration = 1f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator StartFade(AudioSource audioSource, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / fadeDuration);
            yield return null;
        }
        yield break;
    }

    public void FadeToZero()
    {
        StartCoroutine(StartFade(audioSource, 0f));
    }
}
