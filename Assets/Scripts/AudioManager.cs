using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header ("..............Audio Source............")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("..............Audio Clip............")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip attack;
    public AudioClip playerRun;
    public AudioClip collect;
    public AudioClip jump;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayLoopingSFX(AudioClip clip)
    {
        if (SFXSource.clip != clip || !SFXSource.isPlaying)
        {
            SFXSource.clip = clip;
            SFXSource.loop = true;
            SFXSource.Play();
        }
    }

    public void StopLoopingSFX()
    {
        if (SFXSource.isPlaying)
        {
            SFXSource.Stop();
        }
    }

}
