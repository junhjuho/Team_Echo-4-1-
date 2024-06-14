using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NHR
{
    public class EnableInventoryObject : InteractiveObject
    {
        public CustomGrabInteratable customGrab;

        private void Start()
        {
            //CustomGrabInteratable �߰� �� DynamicAttach Ȱ��ȭ
            this.customGrab = this.gameObject.AddComponent<CustomGrabInteratable>();
            this.gameObject.GetComponent<CustomGrabInteratable>().useDynamicAttach = true;

            //�ؽ�Ʈ
            this.txtGo.GetComponent<TMP_Text>().text = "�ݱ�";
            this.txtGo.SetActive(false);
        }
        private void OnBecameVisible()
        {
            if (this.customGrab.isSelectEntered) this.uiGo.SetActive(false);
        }
    }

}
