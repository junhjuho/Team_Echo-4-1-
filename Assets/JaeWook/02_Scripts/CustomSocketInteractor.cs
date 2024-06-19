using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Jaewook
{
    public class CustomSocketInteractable : XRSocketInteractor
    {
<<<<<<< HEAD
        [Header("발동 효과 사운드")]
        public AudioSource audioSource;
        [Header("나가는 문")]
        public Door door;

        private  void Start()
=======
        AudioSource audiosource;
        
        protected override void Start()
>>>>>>> parent of 49b38158 (�뀋)
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
<<<<<<< HEAD
            // 키가 
            if(args.interactorObject.transform.TryGetComponent<FinalKey>(out FinalKey finalKeyComponenet))
            {
                args.interactableObject.transform.gameObject.SetActive(false);

                door.OpenDoor();
            }
            else
            {
                Debug.LogError("이거 키 아닌디?");
            }
=======
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



>>>>>>> parent of 49b38158 (�뀋)
        }

        /*
        private void SelectExit(SelectExitEventArgs args)
        {
            args.interactableObject.transform.GetComponent<MeshRenderer>().enabled = true;

        }
        */
    }

}
