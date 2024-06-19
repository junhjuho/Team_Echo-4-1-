using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Jaewook
{
    public class CustomSocketInteractable : XRSocketInteractor
    {
        AudioSource audiosource;
        
        protected override void Start()
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



        }

        /*
        private void SelectExit(SelectExitEventArgs args)
        {
            args.interactableObject.transform.GetComponent<MeshRenderer>().enabled = true;

        }
        */
    }

}
