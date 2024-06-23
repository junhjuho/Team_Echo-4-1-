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

        //퀘스트 안내 화살표
        [Header("퀘스트 안내 화살표")]
        public Billboard arrowBillboard;

        [Header("튜토리얼 안내 Hands")]
        public TutorialHands tutorialHands;

        private void Awake()
        {
            this.tutorialHands = GetComponentInChildren<TutorialHands>();
            this.arrowBillboard = GetComponentInChildren<Billboard>();
            this.arrowBillboard.gameObject.SetActive(false);
        }
    }


}
