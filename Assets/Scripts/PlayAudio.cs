using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundEffect : MonoBehaviour
{
    public AudioClip soundEffect;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (soundEffect == null)
        {
            Debug.LogWarning("SoundEffect not assigned.");
            return;
        }

        audioSource.PlayOneShot(soundEffect);
    }
}

