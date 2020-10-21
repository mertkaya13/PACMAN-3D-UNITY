using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip pacmanMoveSound;
    public AudioClip pacmanDiesSound;
    public AudioClip pacmanPowerUpSound;
    public AudioClip pacmanEatsGhostSound;
    public AudioClip MenuSound;
    public AudioClip menuButtonSound;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    

    private static SoundManager instance;

    public static SoundManager getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public void playMoveSound(bool control)
    {

        audioSource1.loop = true;
        audioSource1.clip = pacmanMoveSound;
        if(control)
            audioSource1.Play();
        else
        {
            audioSource1.Stop();
        }

    }

    public void playPacmanDiesSound(bool control)
    {

        audioSource2.Stop();
        audioSource2.loop = false;
        audioSource2.clip = pacmanDiesSound;
        if (control)
            audioSource2.PlayOneShot(pacmanDiesSound);
        else
        {
            audioSource2.Stop();
        }

    }

    public void playPacmanEatsGhost(bool control)
    {

        audioSource2.Stop();
        audioSource2.loop = false;
        audioSource2.clip = pacmanEatsGhostSound;
        if (control)
            audioSource2.Play();
        else
        {
            audioSource2.Stop();
        }

    }

    public void playPacmanPowerUpSound(bool control)
    {

        audioSource2.loop = false;
        audioSource2.clip = pacmanPowerUpSound;
        if (control)
            audioSource2.Play();
        else
        {
            audioSource2.Stop();
        }

    }

    public void playMenuSound(bool control)
    {

        audioSource1.loop = true;
        audioSource1.clip = MenuSound;
        if (control)
            audioSource1.Play();
        else
        {
            audioSource1.Stop();
        }

    }

    public void playButtonSound(bool control)
    {

        audioSource2.loop = false;
        audioSource2.clip = menuButtonSound;
        if (control)
            audioSource2.Play();
        else
        {
            audioSource2.Stop();
        }

    }

}
