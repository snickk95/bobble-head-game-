using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager Instance = null;

    private AudioSource soundEffectAudio;

    //sound variables
    public AudioClip gunFire;

    public AudioClip upgradedGunFire;

    public AudioClip marineDeath;

    public AudioClip hurt;

    public AudioClip alienDeath;
   
    public AudioClip victory;

    public AudioClip elevatorArrived;

    public AudioClip powerUpPickup;

    public AudioClip powerUpAppear;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //initalizes an array to get the audio soruce and gets a frefrence to that clip
        AudioSource[] sources = GetComponents<AudioSource>();

        foreach (AudioSource source in sources)
        {
            if (source.clip == null)
            {
                soundEffectAudio = source;
            }
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
