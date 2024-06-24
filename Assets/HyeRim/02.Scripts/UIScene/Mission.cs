using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NHR
{
    public class Mission : MonoBehaviour
    {
        [Header("1단계 미션")]
        public TMP_Text textFirstStep;

        [Header("미션 아이템")]
        public ItemObject targetItem;

        private void Start()
        {
            //미션 완료하면 폰트 바꾸기
            //if (this.targetItem.isFind) this.textFirstStep.fontStyle = FontStyles.Strikethrough;
        }
        public void UpdateMission()
        {
            if (this.targetItem.isFind) this.textFirstStep.fontStyle = FontStyles.Strikethrough;
        }
    }
}
