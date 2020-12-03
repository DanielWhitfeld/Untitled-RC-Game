using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RcSounds : MonoBehaviour
{
    public RcMovement RcMovementReference;
    private float RC_speed;
    public float SoundSpeedThreshold = 0f;

    //creating references to audio sources
    public AudioSource CarIdleSource;
    public AudioSource CarDriveSource;
    public AudioSource CarImpactSource;
    public AudioSource CarDriveStartSource;
    public AudioSource CarRevSource;

    //creating references to specific 'impact' audio clips
    public AudioClip CarImpactClip1;
    public AudioClip CarImpactClip2;
    public AudioClip CarImpactClip3;
    public AudioClip CarImpactClip4;

    public float ImpactMaxVolume = 1.2f;
    public float ImpactMinVolume = 0.7f;

    private float CarIdleCountdown = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IdleToDriveTransition();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 0.5)
        {
            //initializing the sound based on how fast the car is going when it collides
            RCcarImpactSound();
        }
    }

    void RCcarImpactSound()
    {
        //randomizing the choice of sound to play
        int ClipChoice = Random.Range(1, 4);
        switch(ClipChoice)
        {
            case 1: CarImpactSource.clip = CarImpactClip1;
                break;
            case 2: CarImpactSource.clip = CarImpactClip2;
                break;
            case 3: CarImpactSource.clip = CarImpactClip3;
                break;
            case 4: CarImpactSource.clip = CarImpactClip4;
                break;
        }
        CarImpactSource.pitch = Random.Range(0.8f, 1);
        CarImpactSource.volume = Random.Range(ImpactMinVolume, ImpactMaxVolume);
        CarImpactSource.Play();
    }

    void IdleToDriveTransition()
    {
        //transition from drive sound to idle sound
        if (RcMovementReference.m_MovementInputValue == SoundSpeedThreshold)
        {
            if (CarIdleSource.volume != 1f)
            {
                CarIdleSource.volume += 0.25f;
            }
            if (CarDriveSource.volume != 0f)
            {
                CarDriveSource.volume -= 0.2f;
            }
            //countdown to randomly play other idle sounds
            CarIdleCountdown -= 25 * Time.deltaTime;
            if (CarIdleCountdown <= 0f)
            {
                CarIdleCountdown = Random.Range(100, 200);
                CarRevSource.pitch = Random.Range(0.7f, 1.1f);
                CarRevSource.volume = Random.Range(0.6f, 1f);
                CarRevSource.Play();
            }
        }

        if (RcMovementReference.m_MovementInputValue != SoundSpeedThreshold)
        {
            //transition from idle sound to drive sound
            if (CarIdleSource.volume != 0f)
            {
                CarIdleSource.volume -= 0.1f;
            }
            if (CarDriveSource.volume != 0.5f)
            {
                CarDriveSource.volume += 0.025f;
            }
        }
    }
}
