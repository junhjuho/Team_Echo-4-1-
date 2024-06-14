using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaewook
{
    public class Injection : MonoBehaviour, Jaewook.IItem
    {
        public AudioSource audioSourceInjection;
        
        void Start()
        {
            audioSourceInjection = gameObject.AddComponent<AudioSource>();
            audioSourceInjection.clip = Resources.Load<AudioClip>("InjectionSound");
        }

        public void OnGrab()
        {
            // Particle
        }

        public void OnUse()
        {
            // �ֻ��ϴ� ���� ȿ��
            if (audioSourceInjection != null && audioSourceInjection.clip != null)
            {
                audioSourceInjection.Play();
                Debug.Log("�ֻ� ȿ����!");
            }

        }

        public void OnRelease()
        {
            throw new System.NotImplementedException();
        }
    }

}
