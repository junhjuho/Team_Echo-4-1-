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
        #region 우측 UI 표시
        [Header("우측캔버스 메인 타이틀")]
        public TMP_Text titleTextRight;
        [Header("우측캔버스 서브 타이틀")]
        public TMP_Text subTitleTextRight;
        #endregion

        private void Awake()
        {
            // TitleSceneManager.Instance.uiTitleRight = this;

            Transform uiTitleRight = GameObject.Find("UITitleRight")?.transform;

            #region 우측 UI
            if (uiTitleRight == null)
            {
                Debug.LogError("UITitleRight Canvas를 찾을 수 없습니다. Hierarchy에서 UITitleRight 오브젝트의 이름을 확인하세요.");
            }
            else
            {
                titleTextRight = uiTitleRight.Find("TitleTextRight")?.GetComponent<TMP_Text>();
                subTitleTextRight = uiTitleRight.Find("SubTitleTextRight")?.GetComponent<TMP_Text>();

                if (titleTextRight == null)
                    Debug.LogError("TitleTextRight를 찾을 수 없습니다. Hierarchy에서 TitleTextRight 오브젝트의 이름을 확인하세요.");
                if (subTitleTextRight == null)
                    Debug.LogError("SubTitleTextRight를 찾을 수 없습니다. Hierarchy에서 SubTitleTextRight 오브젝트의 이름을 확인하세요.");
            }
            #endregion
        }
    }

}
