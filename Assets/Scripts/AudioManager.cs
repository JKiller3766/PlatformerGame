using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source -----------")]

    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip -----------")]

    public AudioClip gameplay;
    public AudioClip jump;

    private void Start()
    {
        MusicSource.clip = gameplay;
        MusicSource.Play();
    }

    private void OnEnable()
    {
        PlayerJumper.OnJump += PlaySound;
    }

    private void OnDisable()
    {
        PlayerJumper.OnJump -= PlaySound;
    }
    private void PlaySound()
    {
        SFXSource.PlayOneShot(jump);
    }
}