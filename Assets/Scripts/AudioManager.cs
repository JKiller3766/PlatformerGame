using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource MusicSource;
    [SerializeField] public AudioSource SFXSource;

    public AudioClip Gameplay;
    public AudioClip Jump;

    private void Awake()
    {
        MusicSource.clip = Gameplay;
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
        SFXSource.PlayOneShot(Jump);
    }
}