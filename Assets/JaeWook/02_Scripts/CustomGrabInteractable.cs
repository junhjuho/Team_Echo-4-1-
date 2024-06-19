using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jaewook;
using UnityEngine.XR.Interaction.Toolkit;
using System;

namespace Jaewook
{
    /// <summary>
    /// Custom Grab�Ǵ� item�� ����,
    /// Ư�� �κ��� ���� �ʿ䰡 �ִ� item?
    /// </summary>
    public class CustomGrabInteractable : XRGrabInteractable
    {
        
        void Start()
        {
            // base.Start();
            this.selectEntered.AddListener(SelectSomething); // grab
            this.selectExited.AddListener(ReleaseEvent); // not grab
            this.activated.AddListener(ActivatingEvent); // trigger
            //this.deactivated.AddListener(DeactivatingEvent);
        }

        /// <summary>
        /// Active Ȱ�� -> OnUse();
        /// </summary>
        /// <param name="args"></param>
        private void ActivatingEvent(ActivateEventArgs args)
        {
            if (args.interactableObject.transform.GetComponent<IItem>() != null)
            {
                IItem item = args.interactableObject.transform.GetComponent<IItem>();
                item.OnUse();
            }
        }
        private void SelectSomething(SelectEnterEventArgs args)
        {
            if (args.interactableObject.transform.GetComponent<IItem>() != null)
            {
                IItem item = args.interactableObject.transform.GetComponent<IItem>();
                item.OnGrab();

            }
        }

        private void ReleaseEvent(SelectExitEventArgs args)
        {
            if (args.interactableObject.transform.GetComponent<IItem>() != null)
            {
                IItem item = args.interactableObject.transform.GetComponent<IItem>();
                item.OnRelease();
            }
        }
    }
}
