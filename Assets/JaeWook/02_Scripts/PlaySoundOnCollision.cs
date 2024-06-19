using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaewook
{
    /// <summary>
    /// 트리거 발동 스크립트 ( TriggerPoint 프리펩에 연결해서 사용 )
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class PlaySoundOnCollision : MonoBehaviour
    {
        private AudioSource audioSource;
        private bool oncePlayed = false;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        
        private void OnCollisionEnter(Collision collision)
        {
            
            // 충돌이 발생할 때 한번만 사운드 재생
           
            
        }
        
        private void OnTriggerEnter(Collider other)
        {
            // 오브젝트 트리거 활성화
            if (!oncePlayed)
            {
                audioSource.Play();
                Debug.Log("트리거 사운드 실행!");
            }
            else
            {
                Debug.LogError("트리거 사운드 미할당");
            }
            // 한번 트리거 되면 삭제
            this.gameObject.SetActive(false);
        }
    }

}
