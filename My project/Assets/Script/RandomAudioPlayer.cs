using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    void Start()
    {
        //PlayRandomAudio();
    }

    public void PlayRandomAudio()
    {
        if (audioClips.Length == 0)
        {
            Debug.LogWarning("No audio clips assigned.");
            return;
        }

        int randomIndex = Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[randomIndex];
        audioSource.Play();
    }
}
