using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace NHR
{
    public class UIAttacked : MonoBehaviour
    {
        //���� ���� ȿ�� �̹�����
        public Image[] attackedImages;
        //����/���� UI
        public Image imageDeath;
        //�ؽ�Ʈ
        public TMP_Text textState;

        public GameObject uiHeart;
        //�����
        public UIHeart[] hearts;

        public int attackCount = 0;

        public void Init()
        {
            this.imageDeath.gameObject.SetActive(false);
            this.textState.gameObject.SetActive(false);

            foreach (var image in attackedImages)
            {
                image.gameObject.SetActive(false);
            }
            foreach (var heart in this.hearts)
            {
                heart.imageDeath.SetActive(false);
            }

            this.uiHeart.gameObject.SetActive(false);
        }
        public void Close()
        {
            Invoke("CloseUI", 2f);
        }

        private void CloseUI()
        {
            this.imageDeath.gameObject.SetActive(false);
            this.imageDeath.gameObject.SetActive(false);
            this.textState.gameObject.SetActive(false);
            this.uiHeart.SetActive(false);
        }
        public void OpenUI()
        {
            Debug.Log("���� UI ��Ʈ ������Ʈ");
            this.uiHeart.SetActive(true);
            for (int i = 0; i < this.attackCount + 1; i++) this.attackedImages[i].gameObject.SetActive(true);
            this.imageDeath.gameObject.SetActive(true);
            this.textState.gameObject.SetActive(true);
            this.attackCount++;
        }
    }

}
