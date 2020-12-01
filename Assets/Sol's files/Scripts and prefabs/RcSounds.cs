using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RcSounds : MonoBehaviour
{
    public RcMovement RcMovementReference;
    private float RC_speed;

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

    private bool StartedDriving = false;
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

    void IdleToDriveTransition()
    {
        //transition from drive sound to idle sound
        if (RcMovementReference.m_MovementInputValue == 0f)
        {
            if (CarIdleSource.volume != 1f)
            {
                CarIdleSource.volume += 0.25f;
            }
            if (CarDriveSource.volume != 0f)
            {
                CarDriveSource.volume -= 0.2f;
            }
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
        else
        {
            //transition from idle sound to drive sound
            if (CarIdleSource.volume != 0f)
            {
                CarIdleSource.volume -= 0.1f;
            }
            if (CarDriveSource.volume != 0.75f)
            {
                CarDriveSource.volume += 0.025f;
            }
        }
    }
}
