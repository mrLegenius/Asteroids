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

    public void PlayOneShot(AudioClip clip)
    {
        soundEffects.PlayOneShot(clip);
    }
}
