using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoundEffect
{
    Explosion,
    HitMedium,
    Laser,
    LaserCharge,
    CoinPickup,
    ShootLarge,
    Tomato,
    ZomDeath,
    ZomHurt
}
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private List<AudioSource> audioSources = new List<AudioSource>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++) // Create 5 AudioSources
        {
            AudioSource src = gameObject.AddComponent<AudioSource>();
            audioSources.Add(src);
        }
    }
public static void PlaySound(SoundEffect sound, float volume = 1, double duration = 1) 
    { 
        AudioSource freeSource = instance.audioSources.Find(a => !a.isPlaying); // Find an available AudioSource
        if (freeSource != null)
        {
            freeSource.clip = instance.soundList[(int)sound];
            freeSource.volume = volume;
            freeSource.Play();
        }
        if (duration > 0)
        {
            instance.StartCoroutine(instance.StopSoundAfterDuration(freeSource, duration));
        }
    }

    private IEnumerator StopSoundAfterDuration(AudioSource source, double duration)
    {
        yield return new WaitForSeconds((float)duration);
        source.Stop();
    }
}
