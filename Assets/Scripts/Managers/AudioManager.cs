using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource soundEffects;
    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }

    /// <summary>
    /// Запускает clip на soundEffects источнике
    /// </summary>
    /// <param name="clip">Звуковая дорожка</param>
    public void PlayOneShot(AudioClip clip)
    {
        soundEffects.PlayOneShot(clip);
    }
}
