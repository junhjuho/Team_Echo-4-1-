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
        public void OpenUI(int heart)
        {
            this.uiHeart.SetActive(true);
            this.attackedImages[3 - heart].gameObject.SetActive(true);
            this.imageDeath.gameObject.SetActive(true);
            this.textState.gameObject.SetActive(true);
        }
    }

}
