using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource loopSource;
    public AudioSource onceSource;

    public AudioClip[] audioClip1; // 여러 오디오 클립 배열
    public AudioClip audioClip2;   // 반복 재생할 오디오 클립
    public AudioClip audioClip3;   // 시작 시 한 번만 재생할 오디오 클립

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        loopSource = gameObject.AddComponent<AudioSource>();
        onceSource = gameObject.AddComponent<AudioSource>();

        if (audioClip1 != null && audioClip1.Length > 0)
        {
            PlayRandomClip();
        }

        if (audioClip2 != null)
        {
            PlayLoopClip();
        }

        if (audioClip3 != null)
        {
            PlayOnceClip();
        }

    }

    private void PlayRandomClip()
    {
        int randomIndex = Random.Range(0, audioClip1.Length);
        audioSource.clip = audioClip1[randomIndex];
        audioSource.Play();
    }

    private void PlayLoopClip()
    {
        loopSource.clip = audioClip2;
        loopSource.loop = true;
        loopSource.Play();
    }

    private void PlayOnceClip()
    {
        onceSource.clip = audioClip3;
        onceSource.Play();
        //Destroy(onceSource, audioClip3.length);
    }
}