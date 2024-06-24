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
            Debug.LogError("GameInfoCanvas�� ã�� �� �����ϴ�. Hierarchy���� GameInfoCanvas ������Ʈ�� �̸��� Ȯ���ϼ���.");
        }
        else
        {
            // gameInfoImage = gameInfoCanvas.transform.Find("GameInfoImage")?.GetComponent<Image>();
            gameInfoBackButton = uiGameInfo.Find("GameInfoBackButton")?.GetComponent<Button>();

            // if (gameInfoImage == null)
            //  Debug.LogError("GameInfoImage�� ã�� �� �����ϴ�. Hierarchy���� GameInfoImage ������Ʈ�� �̸��� Ȯ���ϼ���.");
            if (gameInfoBackButton == null)
                Debug.LogError("BackButton�� ã�� �� �����ϴ�. Hierarchy���� BackButton ������Ʈ�� �̸��� Ȯ���ϼ���.");
        }

        gameInfoBackButton.onClick.AddListener(() => 
                    TitleSceneManager.Instance.SwitchCanvas(UITitleSceneMenu.FindObjectOfType<Transform>(), uiGameInfo));
        
    }

}
