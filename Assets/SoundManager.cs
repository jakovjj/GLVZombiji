using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoundEffect
{
    Explosion,              //Eksploztija
    HitMedium,              //Udar s zidom
    Laser,                  //Ne koristi se
    LaserCharge,            //Laser charge
    CoinPickup,             //Coinovi
    ShootLarge,             //Weaponi 0, 1
    Tomato,                 //Zombie death
    ZomDeath,               //Nema korist
    ZomHurt,                //Udarac zombija
    SonicBoom,              //Nema korist
    BinaryThing2,           //Binarni napad
    LaserTest,              //Trenutni Laser
    GasAttack,              //Plinski napad
    Ooof                    //Player death
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

    private AudioSource GetFreeAudioSource()
    {
        AudioSource freeSource = audioSources.Find(a => !a.isPlaying);
        if (freeSource == null)
        {
            freeSource = gameObject.AddComponent<AudioSource>();
            audioSources.Add(freeSource);
        }
        return freeSource;
    }
    public static void PlaySound(SoundEffect sound, float volume = 1, double duration = 1)
    {
        if (instance == null || instance.audioSources == null) return;

        AudioSource freeSource = instance.GetFreeAudioSource();
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
