using UnityEngine;

public class OverrideBGMusic : MonoBehaviour
{
    public AudioClip newMusic;
    public float volume = 1f;

    void Start()
    {
        if (BGMusic.instance != null)
        {
            if (newMusic != null)
                BGMusic.instance.PlayNewMusic(newMusic, volume);
            else
                BGMusic.instance.StartCoroutine(BGMusic.instance.FadeOutAndDestroy());
        }
    }
}
