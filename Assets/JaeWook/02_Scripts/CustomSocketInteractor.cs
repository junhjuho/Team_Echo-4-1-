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
        /// 물건을 Socket 적용 시
        /// </summary>
        /// <param name="args"></param>
        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            if (args.interactorObject.transform.TryGetComponent<FinalKey>(out FinalKey keyComponent))
            {
                // mesh renderer 삭제
                args.interactableObject.transform.GetComponent<MeshRenderer>().enabled = false;

                // collider가 있으면 삭제
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
