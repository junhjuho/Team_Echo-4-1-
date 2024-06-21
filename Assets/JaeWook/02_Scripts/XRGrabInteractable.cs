using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Jaewook
{
    public class XRInteractableCustom : XRGrabInteractable
    {

        void Start()
        {
            this.selectEntered.AddListener(SelectEvent);
            this.selectExited.AddListener(ReleaseEvent);
            this.activated.AddListener(ActivatingEvent);

        }


        public void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out PlayerMovement player))
            {
                var canvas = GameDB.Instance.itemInfomationCanvas;
                canvas.transform.position = this.transform.position+(Vector3.up*2f);
                canvas.gameObject.transform.LookAt(player.transform.position);
                canvas.image.SetActive(true);
                canvas.text.gameObject.SetActive(true);
                canvas.text.text = this.gameObject.name;
            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerMovement player))
            {
                var canvas = GameDB.Instance.itemInfomationCanvas;
                canvas.image.SetActive(false);
                canvas.text.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Active È°¼º -> OnUse();
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
        private void SelectEvent(SelectEnterEventArgs args)
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

        //private void DeactovateSomething(DeactivateEventArgs args)
        //{
        //}

    }
}