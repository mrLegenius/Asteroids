using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundEffects;

    /// <summary>
    /// Запускает clip на soundEffects источнике
    /// </summary>
    /// <param name="clip">Звуковая дорожка</param>
    public void PlayOneShot(AudioClip clip)
    {
        soundEffects.PlayOneShot(clip);
    }
}
