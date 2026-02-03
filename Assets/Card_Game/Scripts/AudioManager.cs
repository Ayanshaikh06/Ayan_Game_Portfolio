using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip flip;
    [SerializeField] private AudioClip match;
    [SerializeField] private AudioClip mismatch;
    [SerializeField] private AudioClip gameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayFlip() => source.PlayOneShot(flip);
    public void PlayMatch() => source.PlayOneShot(match);
    public void PlayMismatch() => source.PlayOneShot(mismatch);
    public void PlayGameOver() => source.PlayOneShot(gameOver);
}
