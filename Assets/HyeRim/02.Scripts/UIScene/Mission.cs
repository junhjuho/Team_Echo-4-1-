using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NHR
{
    public class Mission : MonoBehaviour
    {
        [Header("1�ܰ� �̼�")]
        public TMP_Text textFirstStep;

        [Header("�̼� ������")]
        public ItemObject targetItem;

        private void Start()
        {
            //�̼� �Ϸ��ϸ� ��Ʈ �ٲٱ�
            //if (this.targetItem.isFind) this.textFirstStep.fontStyle = FontStyles.Strikethrough;
        }
        public void UpdateMission()
        {
            if (this.targetItem.isFind) this.textFirstStep.fontStyle = FontStyles.Strikethrough;
        }
    }
}
