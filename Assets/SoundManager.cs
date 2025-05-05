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
    ZomHurt,
    SonicBoom
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
        //if (instance == null) instance = this;
       // else Debug.LogError("Multiple SoundManager instances detected!");
    }

    private void Start()
    {
        for (int i = 0; i < 9; i++) 
        {
            AudioSource src = gameObject.AddComponent<AudioSource>();
            audioSources.Add(src);
        }
        //Debug.Log($"SoundList Initialized: {(soundList != null ? "Yes" : "No")}, Length: {soundList?.Length}");
        //Debug.Log($"AudioSource Count: {audioSources.Count}");
    }
    public static void PlaySound(SoundEffect sound, float volume = 1, double duration = 1)
    {
        if (instance == null || instance.audioSources == null) return;

        AudioSource freeSource = instance.audioSources.Find(a => !a.isPlaying);
        if (freeSource == null) return;

        freeSource.clip = instance.soundList[(int)sound];
        freeSource.volume = volume;
        freeSource.Play();

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
