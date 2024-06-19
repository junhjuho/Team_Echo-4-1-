using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Jaewook
{
    public class CustomSocketInteractor : XRSocketInteractor
    {
        [Header("발동 효과 사운드")]
        public AudioSource audioSource;
        [Header("나가는 문")]
        public Door door;

        private  void Start()
        {
            this.selectEntered.AddListener(OnSelectEntered);
        }

        private void OnSelectEntered(SelectEnterEventArgs args)
        {
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
        }
    }

}
