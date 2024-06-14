using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NHR
{
    public class UIGameEvent : MonoBehaviour
    {
        public UINotice uiNotice;
        public UIRole uiRole;

        //생존자
        public UIAttacked uiAttacked;

        //괴물
        public UIMonsterMode uiMonsterMode;

        //관전UI
        public UIWatching uiWatching;

        private GameObject nowPopUI;

        private void Awake()
        {
            //임시
            DataManager.Instance.LoadEventDialogData();

            this.uiNotice = FindObjectOfType<UINotice>();
            this.uiRole = FindObjectOfType<UIRole>();
            this.uiAttacked = FindObjectOfType<UIAttacked>();
            this.uiMonsterMode = FindObjectOfType<UIMonsterMode>();
            this.uiWatching = FindObjectOfType<UIWatching>();

            this.Init();
        }
        private void Init()
        {
            this.uiNotice.Init();
            this.uiNotice.gameObject.SetActive(false);

            this.uiRole.Init();
            this.uiRole.gameObject.SetActive(false);

            this.uiAttacked.Init();

            this.uiMonsterMode.gameObject.SetActive(false);
            this.uiWatching.gameObject.SetActive(false);
        }
        private void Start()
        {
            //이벤트 string 전달
            EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Notice_EventUI, new EventHandler<string>((type, str) =>
            {
                this.uiNotice.Init();
                Debug.Log("<color=yellow>notice</color>");
                var dialog = DataManager.Instance.GetEventDialog(str);
                Debug.LogFormat("<color=yellow>{0}</color>", dialog);
                this.uiNotice.gameObject.SetActive(true);
                this.nowPopUI = this.uiNotice.gameObject;
                StartCoroutine(CTypingDialog(dialog, this.uiNotice.textNotice));
                //this.uiNotice.textNotice.text = dialog;
            }));
            //역할 전달
            EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Notice_Role, new EventHandler<string>((type, role) =>
            {
                this.uiRole.Init();
                Debug.Log("<color=yellow>역할 배정 완료</color>");
                //role은 Runner와 Chaser
                var roleDialog = DataManager.Instance.GetEventDialog("role" + role);
                var questDialog = DataManager.Instance.GetEventDialog("quest" + role);

                this.uiRole.gameObject.SetActive(true);
                this.nowPopUI = this.uiRole.gameObject;
                Debug.LogFormat("<color=yellow>{0}</color>", role);
                this.uiRole.textRole.text = roleDialog;
                StartCoroutine(CTypingDialog("주 목표 : " + questDialog, this.uiRole.textQuest));
            }));

            //공격받음 1, 2, 3단계
            EventDispatcher.instance.AddEventHandler<int>((int)NHR.EventType.eEventType.Notice_Attacked, new EventHandler<int>((type, heart) =>
            {
                Debug.Log("<color=red>공격 받음</color>");

                //UI열기
                this.uiAttacked.OpenUI(heart);

                //목숨 줄어드는 UI
                this.uiAttacked.hearts[3 - heart].imageDeath.SetActive(true);

                //기절 혹은 죽음 UI text
                if (heart != 1) this.uiAttacked.textState.text = DataManager.Instance.GetEventDialog("attacked");
                else
                {
                    this.uiAttacked.textState.text = DataManager.Instance.GetEventDialog("death");
                    Invoke("PopWatching", 2f);
                }
                this.uiAttacked.Close();
            }));

            //부활
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Notice_Respawn, new EventHandler((type) =>
            {
                Debug.Log("<color=red>부활</color>");
                //기절 UI 없애기
                this.uiAttacked.imageDeath.gameObject.SetActive(false);
            }));

            //관전(죽은 후, 승리 후 다른 플레이어 기다림)
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Watching_Game, new EventHandler((type) =>
            {
                Debug.Log("<color=blue>관전모드</color>");

            }));
        }

        private void PopWatching()
        {
            this.uiWatching.gameObject.SetActive(true);
        }

        //dialog 출력
        IEnumerator CTypingDialog(string dialog, TMP_Text tmp)
        {
            foreach (var c in dialog)
            {
                tmp.text += c;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(2f);
            //this.nowPopUI.gameObject.SetActive(false);
        }
    }
}
