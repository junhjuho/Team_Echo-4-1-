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
        //public enum Axis
        //{
        //    X,
        //    Y,
        //    Z
        //}

        //public Axis lookAxis = Axis.Y;

        //public Transform targetTf;
        //public bool isTargetCamera = true;

        //public GameObject uiGo;
        //public GameObject txtGo;

        void Start()
        {
            this.selectEntered.AddListener(SelectEvent);
            this.selectExited.AddListener(ReleaseEvent);
            this.activated.AddListener(ActivatingEvent);

            //if (isTargetCamera)
            //{
            //    targetTf = Camera.main.transform;
            //}

            //this.txtGo.SetActive(false);
            //this.uiGo.SetActive(false);
            //this.txtGo.GetComponent<TMP_Text>().text = this.name;

        }
        //private void OnBecameVisible()
        //{
        //    //�þ߰��� ������ �� ��ȣ�ۿ� ���� UI ������
        //    Debug.Log("OnBecameVisible");
        //    this.uiGo.SetActive(true);
        //    //StartCoroutine(CLookCamera(this.uiGo));
        //}
        //private void OnBecameInvisible()
        //{
        //    //�þ߰����� ���������� �� ��ȣ�ۿ� ���� UI ������
        //    Debug.Log("OnBecameInvisible");
        //    this.uiGo.SetActive(true);
        //    //StopAllCoroutines();
        //}

        //public void OnTriggerEnter(Collider other)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        Debug.Log("PlayerCollision");
        //        this.txtGo.SetActive(true);
        //    }
        //}

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