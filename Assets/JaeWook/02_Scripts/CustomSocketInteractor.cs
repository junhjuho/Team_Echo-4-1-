using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Jaewook
{
    public class CustomSocketInteractable : XRSocketInteractor
    {
<<<<<<< HEAD
        [Header("�ߵ� ȿ�� ����")]
        public AudioSource audioSource;
        [Header("������ ��")]
        public Door door;

        private  void Start()
=======
        AudioSource audiosource;
        
        protected override void Start()
>>>>>>> parent of 49b38158 (ㅇ)
        {
            this.selectEntered.AddListener(OnSelectEntered);
            // this.selectExited.AddListener(SelectExit);
        }

        /// <summary>
        /// ������ Socket ���� ��
        /// </summary>
        /// <param name="args"></param>
        private void OnSelectEntered(SelectEnterEventArgs args)
        {
<<<<<<< HEAD
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
=======
            if (args.interactorObject.transform.TryGetComponent<FinalKey>(out FinalKey keyComponent))
            {
                // mesh renderer ����
                args.interactableObject.transform.GetComponent<MeshRenderer>().enabled = false;

                // collider�� ������ ����
                if (args.interactableObject.transform.GetComponent<Collider>() != null)
                {
                    args.interactableObject.transform.GetComponent<Collider>().enabled = false;
                }

                args.interactableObject.transform.gameObject.SetActive(false);
                


            }



>>>>>>> parent of 49b38158 (ㅇ)
        }

        /*
        private void SelectExit(SelectExitEventArgs args)
        {
            args.interactableObject.transform.GetComponent<MeshRenderer>().enabled = true;

        }
        */
    }

}
