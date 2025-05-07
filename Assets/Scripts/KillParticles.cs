using UnityEngine;

public class KillParticles : MonoBehaviour
{
    private ParticleSystem particles;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource != null) audioSource.Play();
        particles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (particles && !particles.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}