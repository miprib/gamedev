using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    static AudioSource[] audioSrcArr;
    static AudioSource actionAudioSrc, jumpAudioSrc, walkAudioSrc;
    public static AudioClip playerHitSound, walkSound, fireSound, jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSrcArr = GetComponents<AudioSource>();

        actionAudioSrc = audioSrcArr[0];
        jumpAudioSrc = audioSrcArr[1];
        walkAudioSrc = audioSrcArr[2];

        playerHitSound = Resources.Load<AudioClip>("Splat Crush 01");
        walkSound = Resources.Load<AudioClip>("FOOTSTEPS (A) Walking Loop 01");
        fireSound = Resources.Load<AudioClip>("SWIPE Whoosh Double 01");
        jumpSound = Resources.Load<AudioClip>("BOUNCE Twang Spring 13");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "playerHit":
                actionAudioSrc.PlayOneShot(playerHitSound);
                break;
            case "fire":
                actionAudioSrc.PlayOneShot(fireSound);
                break;

            case "jump":
                if (jumpAudioSrc.isPlaying)
                {
                    jumpAudioSrc.Stop();
                }
                jumpAudioSrc.PlayOneShot(jumpSound);
                break;

            case "walk":
                if (!walkAudioSrc.isPlaying)
                {
                    walkAudioSrc.PlayOneShot(walkSound);
                }
                break;
            case "stopWalk":
                if (walkAudioSrc.isPlaying)
                {
                    walkAudioSrc.Stop();
                }
                break;
        }
    }
}
