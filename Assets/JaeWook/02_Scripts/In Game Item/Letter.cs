using System.Collections;
using System.Collections.Generic;
using Jaewook;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jaewook
{
    public class Letter : MonoBehaviour, IItem
    {
        public bool isOnMap = false;
        public TMP_Text letterUI;
        public Image image;

        private void Start()
        {
            letterUI = GetComponentInChildren<TMP_Text>();
            image = GetComponentInChildren<Image>();

            letterUI.text = " 11 ";

            // LetterUI�� ��Ȱ��ȭ �������� Ȯ���մϴ�.
            if (letterUI != null)
            {
                image.enabled = isOnMap;
                letterUI.enabled = isOnMap;
            }
            // letterUI�� Inspector�Ҵ� Ȯ��
            else if (letterUI == null)
            {
                Debug.LogError("LetterUI�� ����");
            }
        }

        public void OnGrab()
        {
            // ���� �׷��� ������ �ִ� -> �׷��Լ��� �۵����� -> custom interactable ����?
            /*
            if (letterUI != null)
            {
                isOnMap = !isOnMap;
                image.enabled = isOnMap;
                letterUI.enabled = isOnMap;
                Debug.Log("�α�Ȯ�ο� (LetterUI Ȱ��ȭ - ��Ȱ��ȭ ����)");
            }
            */
        }

        public void OnUse()
        {
           
            if (letterUI != null)
            {
                isOnMap = !isOnMap;
                image.enabled = isOnMap;
                letterUI.enabled = isOnMap;
                Debug.Log("�α�Ȯ�ο� (LetterUI Ȱ��ȭ - ��Ȱ��ȭ ����)");
            }
        }

        public void OnRelease()
        {
            // �������� ������ �� UI�� ��Ȱ��ȭ

            letterUI.enabled = false;


            // ������ ��ü ���� ?
            // this.transform.SetActive(false);
        }

    }
}
