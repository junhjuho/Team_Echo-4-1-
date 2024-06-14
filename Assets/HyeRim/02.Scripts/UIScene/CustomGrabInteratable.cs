using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace NHR
{
    public class CustomGrabInteratable : XRGrabInteractable
    {
        public bool isSelectEntered = false;

        //�ӽ�
        private GameObject uiItem;

        private void Start()
        {
            this.selectEntered.AddListener(SelectEntered);
            this.selectExited.AddListener(SelectExited);

            //�ӽ�
            this.uiItem = this.gameObject.GetComponent<EnableInventoryObject>().uiGo;
        }
        private void SelectEntered(SelectEnterEventArgs args)
        {
            //Debug.Log("SelectEntered");
            //�̺�Ʈ ���߿� �ٲٱ�
            this.uiItem.SetActive(false);
            this.isSelectEntered = true;
        }
        private void SelectExited(SelectExitEventArgs args)
        {
            this.uiItem.SetActive(true);
        }
    }

}
