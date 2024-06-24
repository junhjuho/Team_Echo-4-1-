using System.Collections;
using System.Collections.Generic;
using Jaewook;
using UnityEngine;
using UnityEngine.UI;
using SeongMin;


public class UIGameInfo : MonoBehaviour
{
    public Button gameInfoBackButton;

    private void Awake()
    {
        TitleSceneManager.Instance.uiGameInfo = this;

        Transform uiGameInfo = GameObject.Find("GameInfoCanvas")?.transform;

        if (uiGameInfo == null)
        {
            Debug.LogError("GameInfoCanvas를 찾을 수 없습니다. Hierarchy에서 GameInfoCanvas 오브젝트의 이름을 확인하세요.");
        }
        else
        {
            // gameInfoImage = gameInfoCanvas.transform.Find("GameInfoImage")?.GetComponent<Image>();
            gameInfoBackButton = uiGameInfo.Find("GameInfoBackButton")?.GetComponent<Button>();

            // if (gameInfoImage == null)
            //  Debug.LogError("GameInfoImage를 찾을 수 없습니다. Hierarchy에서 GameInfoImage 오브젝트의 이름을 확인하세요.");
            if (gameInfoBackButton == null)
                Debug.LogError("BackButton을 찾을 수 없습니다. Hierarchy에서 BackButton 오브젝트의 이름을 확인하세요.");
        }

        gameInfoBackButton.onClick.AddListener(() => 
                    TitleSceneManager.Instance.SwitchCanvas(UITitleSceneMenu.FindObjectOfType<Transform>(), uiGameInfo));
        
    }

}
