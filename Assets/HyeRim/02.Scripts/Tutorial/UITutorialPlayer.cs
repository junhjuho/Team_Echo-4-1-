using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NHR
{
    public class UITutorialPlayer : MonoBehaviour
    {
        //Dialog UI
        [Header("Dialog Text")]
        public TMP_Text textDialog;

        //����Ʈ �ȳ� ȭ��ǥ
        [Header("����Ʈ �ȳ� ȭ��ǥ")]
        public Billboard arrowBillboard;

        [Header("Ʃ�丮�� �ȳ� Hands")]
        public TutorialHands tutorialHands;

        private void Awake()
        {
            this.tutorialHands = GetComponentInChildren<TutorialHands>();
            this.arrowBillboard = GetComponentInChildren<Billboard>();
            this.arrowBillboard.gameObject.SetActive(false);
        }
    }


}
