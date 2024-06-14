using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NHR
{
    public class UIClothesColor : MonoBehaviour
    {
        public int index;
        public Button btnColor;
        public GameObject selectedGo;
        //���߿� �������� �ٲ� ��
        public string textureName;
        private void Awake()
        {
            this.btnColor = this.GetComponent<Button>();
            Image[] gos = this.GetComponentsInChildren<Image>();
            this.selectedGo = gos[1].gameObject;
            this.selectedGo.SetActive(false);
        }
    }

}
