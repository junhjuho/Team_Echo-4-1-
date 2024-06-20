using NHR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static NHR.App;

namespace SeongMin
{
    public class UITitleSceneMenu : MonoBehaviour
    {
        public Button startButton;
        public Button tutorialButton;
        public Button gameInfoButton;

        // public Button characterSettingButton;
        // public Button gameSettingButton;

        // 키 입력은 다른 씬으로 넘김
        //public UIKeyboard keyboard;

        private void Awake()
        {
            UIManager.Instance.titleSceneMenu = this;
            startButton = transform.Find("StartButton").GetComponent<Button>();
            tutorialButton = transform.Find("TutorialButton").GetComponent<Button>();
            gameInfoButton = transform.Find("GameInfoButton").GetComponent<Button>();

            //characterSettingButton = transform.Find("CharacterSettingButton").GetComponent<Button>();
            //gameSettingButton = transform.Find("GameSettingButton").GetComponent<Button>();

            // keyboard = FindObjectOfType<UIKeyboard>();

            // 로비 씬 전환 버튼 할당
            startButton.onClick.AddListener(() => 
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Lobby));
            
            // 튜토리얼 - 1라운드 합침
            tutorialButton.onClick.AddListener(() => 
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Tutorial));
            
            /* 폐기
            characterSettingButton.onClick.AddListener(() => 
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.CharacterCustom));
            */
        }

    }

}
