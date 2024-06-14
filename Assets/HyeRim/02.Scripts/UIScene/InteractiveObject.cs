using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace NHR
{
    public class InteractiveObject : MonoBehaviour
    {
        public GameObject uiGo;
        public GameObject txtGo;
        //public GameObject playerGo;

        private void Start()
        {
            this.txtGo.SetActive(false);
            this.uiGo.SetActive(false);
            this.txtGo.GetComponent<TMP_Text>().text = this.name;


        }

        private void OnBecameVisible()
        {
            //�þ߰��� ������ �� ��ȣ�ۿ� ���� UI ������
            Debug.Log("OnBecameVisible");
            this.uiGo.SetActive(true);
            StartCoroutine(CLookCamera(this.uiGo));
        }
        private void OnBecameInvisible()
        {
            //�þ߰����� ���������� �� ��ȣ�ۿ� ���� UI ������
            Debug.Log("OnBecameInvisible");
            this.uiGo.SetActive(true);
            StopAllCoroutines();
        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player");
                this.txtGo.SetActive(true);
            }
        }
        public void OnTriggerExit(Collider other)
        {
            this.txtGo.SetActive(false);
        }
        private IEnumerator CLookCamera(GameObject go)
        {
            while (true)
            {
                Vector3 dir = (go.transform.position -  Camera.main.transform.position).normalized;
                this.uiGo.transform.rotation = Quaternion.LookRotation(dir);
                yield return null;
            }
        }
    }

}
