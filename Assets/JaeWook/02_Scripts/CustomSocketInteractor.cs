using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Jaewook
{
    public class CustomSocketInteractor : XRSocketInteractor
    {
        [Header("�ߵ� ȿ�� ����")]
        public AudioSource audioSource;
        [Header("������ ��")]
        public Door door;

        private  void Start()
        {
            this.selectEntered.AddListener(OnSelectEntered);
        }

        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            // Ű�� 
            if(args.interactorObject.transform.TryGetComponent<FinalKey>(out FinalKey finalKeyComponenet))
            {
                args.interactableObject.transform.gameObject.SetActive(false);

                door.OpenDoor();
            }
            else
            {
                Debug.LogError("�̰� Ű �ƴѵ�?");
            }
        }
    }

}
