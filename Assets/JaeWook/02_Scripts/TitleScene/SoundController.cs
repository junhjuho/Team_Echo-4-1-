using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaewook
{
    public class SoundController : MonoBehaviour
    {
        // public AudioSource audioSource;
        public AudioSource loopSource;
        public AudioSource onceSource;

        // public AudioClip[] audioClip1; // 여러 오디오 클립 배열
        public AudioClip audioClip2;   // 반복 재생할 오디오 클립
        public AudioClip audioClip3;   // 시작 시 한 번만 재생할 오디오 클립

        private void Awake()
        {
            // 오디오 클립 이름으로 할당
            audioClip2 = Resources.Load<AudioClip>("AmbienceMid");
            audioClip3 = Resources.Load<AudioClip>("Bells03");

            if (audioClip2 == null) Debug.LogError("AmbienceMid 오디오 클립을 찾을 수 없습니다.");
            if (audioClip3 == null) Debug.LogError("Bells03 오디오 클립을 찾을 수 없습니다.");
        }

        private void Start()
        {
            // audioSource = gameObject.AddComponent<AudioSource>();
            loopSource = gameObject.AddComponent<AudioSource>();
            onceSource = gameObject.AddComponent<AudioSource>();

            /*
            if (audioClip1 != null && audioClip1.Length > 0)
            {
                PlayRandomClip();
            }
            */

            if (audioClip2 != null)
            {
                PlayLoopClip();
            }

            if (audioClip3 != null)
            {
                PlayOnceClip();
            }
        }

        /*
        private void PlayRandomClip()
        {
            int randomIndex = Random.Range(0, audioClip1.Length);
            audioSource.clip = audioClip1[randomIndex];
            audioSource.Play();
        }
        */

        public void PlayLoopClip()
        {
            loopSource.clip = audioClip2;
            loopSource.loop = true;
            loopSource.Play();
        }

        public void PlayOnceClip()
        {
            onceSource.clip = audioClip3;
            onceSource.PlayOneShot(onceSource.clip);
        }

        public void StopLoopClip()
        {
            if (loopSource.isPlaying)
            {
                loopSource.Stop();
            }
        }

        public void StartLoopClip()
        {
            if (!loopSource.isPlaying)
            {
                loopSource.Play();
            }
        }
        
    }
}
