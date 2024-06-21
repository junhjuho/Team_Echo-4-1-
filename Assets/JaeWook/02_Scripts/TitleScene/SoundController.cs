using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = GetComponent<AudioClip[]>();

        if (audioClip[i] != null)
        {
            for (int i=0; i<audioClip.Length; i++)
            {
                audioSource.clip = audioClip[i];
            }

        }
    }
}
