using SeongMin;
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
            //this.uiGameResult = FindObjectOfType<UIGameResult>();
            //this.Init();
        }
        private void Init()
        {
            this.uiGameResult.gameObject.SetActive(false);
        }

        private void Start()
        {
            //게임이 끝나고 돌아왔다면
            if (GameDB.Instance.hasGameData)
            {
                Debug.Log("game result");
                this.ShowGameResult();
                GameDB.Instance.hasGameData = false;
            }
            else this.gameObject.SetActive(false);

            this.uiGameResult.buttonStay.onClick.AddListener(() => this.uiGameResult.gameObject.SetActive(false));
            this.uiGameResult.buttonTitle.onClick.AddListener(() =>
            {
                //타이틀로 돌아가기
                EventDispatcher.instance.SendEvent<App.eSceneType>((int)NHR.EventType.eEventType.Change_Scene, App.eSceneType.Title);
            });
        }

        private void ShowGameResult()
        {
            this.uiGameResult.gameObject.SetActive(true);

            ;
            if (GameDB.Instance.isWin) this.uiGameResult.textResult.text = "승리";
            else this.uiGameResult.textResult.text = "패배";

            Debug.LogFormat("<color=yellow>게임 결과{0}</color>", GameDB.Instance.isWin);
        }
    }

}
