using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaewook
{
    /// <summary>
    /// Ʈ���� �ߵ� ��ũ��Ʈ ( TriggerPoint �����鿡 �����ؼ� ��� )
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
            
            // �浹�� �߻��� �� �ѹ��� ���� ���
           
            
        }
        
        private void OnTriggerEnter(Collider other)
        {
            // ������Ʈ Ʈ���� Ȱ��ȭ
            if (!oncePlayed)
            {
                audioSource.Play();
                Debug.Log("Ʈ���� ���� ����!");
            }
            else
            {
                Debug.LogError("Ʈ���� ���� ���Ҵ�");
            }
            // �ѹ� Ʈ���� �Ǹ� ����
            this.gameObject.SetActive(false);
        }
    }

}
