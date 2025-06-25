using UnityEngine;
using UnityEngine.Events;



/// <summary>
/// DelayedSoundEvent
/// -----------------
/// Attach this to a GameObject with an AudioSource.
/// 
/// Use case:
/// - Call PlaySoundWithDelay() to trigger sound after `delayInSeconds`.
/// - Hook other responses to `onSoundPlayed` UnityEvent in the Inspector.
/// 
/// Setup Steps:
/// 1. Add this script to a GameObject.
/// 2. Assign an AudioClip to `soundEffect`.
/// 3. Set `delayInSeconds` to desired delay.
/// 4. (Optional) Hook up UnityEvents to `onSoundPlayed` via Inspector.
/// 5. Call PlaySoundWithDelay() from code, button, animation, etc.
///
/// Notes:
/// - Requires AudioSource on same GameObject.
/// - Uses Invoke(). Swap for coroutine if more control is needed.
/// </summary>

[RequireComponent(typeof(AudioSource))]
public class DelayedSoundEvent : MonoBehaviour
{
    public AudioClip soundEffect;
    public float delayInSeconds = 2f;
    public UnityEvent onSoundPlayed;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("Missing AudioSource component.");
        }
    }

    public void PlaySoundWithDelay()
    {
        if (audioSource == null || soundEffect == null)
        {
            Debug.LogWarning("Cannot play sound. AudioSource or AudioClip is missing.");
            return;
        }

        Invoke(nameof(PlaySound), delayInSeconds);
    }

    private void PlaySound()
    {
        audioSource.PlayOneShot(soundEffect);
        onSoundPlayed?.Invoke();
    }
}

