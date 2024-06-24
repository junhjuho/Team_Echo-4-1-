using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jaewook;
using SeongMin;

namespace Jaewook
{
    public class UIMakers : MonoBehaviour
    {
        public Button makersBackButton;

        private void Awake()
        {
            TitleSceneManager.Instance.uiMakers = this;

            Transform uiMakers = GameObject.Find("MakersCanvas")?.transform;

            #region Makers Ui
            if (uiMakers == null)
            {
                Debug.LogError("MakersCanvas를 찾을 수 없습니다. Hierarchy에서 MakersCanvas 오브젝트의 이름을 확인하세요.");
            }
            else
            {
                // makersImage = makersCanvas.transform.Find("MakersImage")?.GetComponent<Image>();
                makersBackButton = uiMakers.Find("MakersBackButton")?.GetComponent<Button>();

                //if (makersImage == null)
                // Debug.LogError("MakersImage를 찾을 수 없습니다. Hierarchy에서 MakersImage 오브젝트의 이름을 확인하세요.");
                if (makersBackButton == null)
                    Debug.LogError("BackButton을 찾을 수 없습니다. Hierarchy에서 BackButton 오브젝트의 이름을 확인하세요.");
            }
            #endregion

            makersBackButton.onClick.AddListener(() => 
                    TitleSceneManager.Instance.SwitchCanvas(UITitleSceneMenu.FindObjectOfType<Transform>(), uiMakers));
        }

        private void Start()
        {
            #region 애니메이션
            
            #endregion
        }

        //
    }

}
