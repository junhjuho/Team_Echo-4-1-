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
                Debug.LogError("MakersCanvas�� ã�� �� �����ϴ�. Hierarchy���� MakersCanvas ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
            else
            {
                // makersImage = makersCanvas.transform.Find("MakersImage")?.GetComponent<Image>();
                makersBackButton = uiMakers.Find("MakersBackButton")?.GetComponent<Button>();

                //if (makersImage == null)
                // Debug.LogError("MakersImage�� ã�� �� �����ϴ�. Hierarchy���� MakersImage ������Ʈ�� �̸��� Ȯ���ϼ���.");
                if (makersBackButton == null)
                    Debug.LogError("BackButton�� ã�� �� �����ϴ�. Hierarchy���� BackButton ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
            #endregion

            makersBackButton.onClick.AddListener(() => 
                    TitleSceneManager.Instance.SwitchCanvas(UITitleSceneMenu.FindObjectOfType<Transform>(), uiMakers));
        }

        private void Start()
        {
            #region �ִϸ��̼�
            
            #endregion
        }

        //
    }

}
