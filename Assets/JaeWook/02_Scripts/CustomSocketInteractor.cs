using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Jaewook
{
    public class CustomSocketInteractor : XRSocketInteractor
    {
        public AudioSource audioSource;
        public Door door;

        private void Start()
        {
            this.selectEntered.AddListener(OnSelectEntered);
        }

        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            if
        }
    }

}
