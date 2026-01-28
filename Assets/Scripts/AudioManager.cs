using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip gameplay;
    public AudioClip jump;

    private void Awake()
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