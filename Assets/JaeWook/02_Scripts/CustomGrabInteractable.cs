using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jaewook;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using TMPro;
using UnityEngine.Animations.Rigging;
using SeongMin;

namespace Jaewook
{
    /// <summary>
    /// Custom Grab�Ǵ� item�� ����,
    /// Ư�� �κ��� ���� �ʿ䰡 �ִ� item?
    /// </summary>
    public class CustomGrabInteractable : XRGrabInteractable
    {
        public enum Axis
        {
            X,
            Y,
            Z
        }

        public Axis lookAxis = Axis.Y;

        public Transform targetTf;
        public bool isTargetCamera = true;

        public GameObject uiGo;
        public GameObject txtGo;

        void Start()
        {
            // base.Start();
            this.selectEntered.AddListener(SelectSomething); // grab
            this.selectExited.AddListener(ReleaseEvent); // not grab
            this.activated.AddListener(ActivatingEvent); // trigger
            //this.deactivated.AddListener(DeactivatingEvent);

            if (isTargetCamera)
            {
                targetTf = Camera.main.transform;
            }

            this.txtGo.SetActive(false);
            this.uiGo.SetActive(false);
            this.txtGo.GetComponent<TMP_Text>().text = this.name;

        }
        private void OnBecameVisible()
        {
            //�þ߰��� ������ �� ��ȣ�ۿ� ���� UI ������
            Debug.Log("OnBecameVisible");
            this.uiGo.SetActive(true);
            //StartCoroutine(CLookCamera(this.uiGo));
        }
        private void OnBecameInvisible()
        {
            //�þ߰����� ���������� �� ��ȣ�ۿ� ���� UI ������
            Debug.Log("OnBecameInvisible");
            this.uiGo.SetActive(true);
            //StopAllCoroutines();
        }

        PlayerData plaerData;
        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("PlayerCollision");
                this.txtGo.SetActive(true);
            }
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
