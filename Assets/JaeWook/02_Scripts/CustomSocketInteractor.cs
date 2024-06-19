using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Jaewook
{
    public class CustomSocketInteractable : XRSocketInteractor
    {
<<<<<<< HEAD
        [Header("¹ßµ¿ È¿°ú »ç¿îµå")]
        public AudioSource audioSource;
        [Header("³ª°¡´Â ¹®")]
        public Door door;

        private  void Start()
=======
        AudioSource audiosource;
        
        protected override void Start()
>>>>>>> parent of 49b38158 (ã…‡)
        {
            this.selectEntered.AddListener(OnSelectEntered);
            // this.selectExited.AddListener(SelectExit);
        }

        /// <summary>
        /// ¹°°ÇÀ» Socket Àû¿ë ½Ã
        /// </summary>
        /// <param name="args"></param>
        private void OnSelectEntered(SelectEnterEventArgs args)
        {
<<<<<<< HEAD
            // Å°°¡ 
            if(args.interactorObject.transform.TryGetComponent<FinalKey>(out FinalKey finalKeyComponenet))
            {
                args.interactableObject.transform.gameObject.SetActive(false);

                door.OpenDoor();
            }
            else
            {
                Debug.LogError("ÀÌ°Å Å° ¾Æ´Ñµð?");
            }
=======
            if (args.interactorObject.transform.TryGetComponent<FinalKey>(out FinalKey keyComponent))
            {
                // mesh renderer »èÁ¦
                args.interactableObject.transform.GetComponent<MeshRenderer>().enabled = false;

                // collider°¡ ÀÖÀ¸¸é »èÁ¦
                if (args.interactableObject.transform.GetComponent<Collider>() != null)
                {
                    args.interactableObject.transform.GetComponent<Collider>().enabled = false;
                }

                args.interactableObject.transform.gameObject.SetActive(false);
                


            }



>>>>>>> parent of 49b38158 (ã…‡)
        }

        /*
        private void SelectExit(SelectExitEventArgs args)
        {
            args.interactableObject.transform.GetComponent<MeshRenderer>().enabled = true;

        }
        */
    }

}
