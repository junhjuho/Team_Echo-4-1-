using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SeongMin;
using static NHR.App;

namespace Jaewook
{
    public class UITitleRight : MonoBehaviour
    {
        #region ���� UI ǥ��
        [Header("����ĵ���� ���� Ÿ��Ʋ")]
        public TMP_Text titleTextRight;
        [Header("����ĵ���� ���� Ÿ��Ʋ")]
        public TMP_Text subTitleTextRight;
        #endregion

        private void Awake()
        {
            // TitleSceneManager.Instance.uiTitleRight = this;

            Transform uiTitleRight = GameObject.Find("UITitleRight")?.transform;

            #region ���� UI
            if (uiTitleRight == null)
            {
                Debug.LogError("UITitleRight Canvas�� ã�� �� �����ϴ�. Hierarchy���� UITitleRight ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
            else
            {
                titleTextRight = uiTitleRight.Find("TitleTextRight")?.GetComponent<TMP_Text>();
                subTitleTextRight = uiTitleRight.Find("SubTitleTextRight")?.GetComponent<TMP_Text>();

                if (titleTextRight == null)
                    Debug.LogError("TitleTextRight�� ã�� �� �����ϴ�. Hierarchy���� TitleTextRight ������Ʈ�� �̸��� Ȯ���ϼ���.");
                if (subTitleTextRight == null)
                    Debug.LogError("SubTitleTextRight�� ã�� �� �����ϴ�. Hierarchy���� SubTitleTextRight ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
            #endregion
        }
    }

}
