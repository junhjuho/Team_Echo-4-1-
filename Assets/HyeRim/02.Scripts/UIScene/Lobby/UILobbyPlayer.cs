using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NHR
{
    public class UILobbyPlayer : MonoBehaviour
    {
        public UIGameResult uiGameResult;

        private void Awake()
        {
            this.uiGameResult = FindObjectOfType<UIGameResult>();
            this.Init();
        }
        private void Init()
        {
            this.uiGameResult.gameObject.SetActive(false);
        }

        private void Start()
        {
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Notice_GameResult, new EventHandler((type) =>
            {
                Debug.Log("game result");
                this.uiGameResult.gameObject.SetActive(true);
            }));
            this.uiGameResult.buttonStay.onClick.AddListener(() => this.uiGameResult.gameObject.SetActive(false));
            this.uiGameResult.buttonTitle.onClick.AddListener(() =>
            {
                //타이틀로 돌아가기
                EventDispatcher.instance.SendEvent<App.eSceneType>((int)NHR.EventType.eEventType.Change_Scene, App.eSceneType.Title);
            });
        }
    }

}
