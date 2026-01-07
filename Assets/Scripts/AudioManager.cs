using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource attackAudioSource;
    //public AudioSource explosionAudioSource;

    public void PlayAttackSound()
    {
        attackAudioSource.Play();
    }

    //public void PlayExplosionSound()
    //{
    //    explosionAudioSource.Play();
    //}

}
