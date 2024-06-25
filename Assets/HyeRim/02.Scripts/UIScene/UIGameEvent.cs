using Photon.Pun;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NHR
{
    public class UIGameEvent : MonoBehaviour
    {
        [Header("안내 팝업 이벤트")]
        public UINotice uiNotice;

        [Header("역할 배정 이벤트")]
        public UIRole uiRole;

        //생존자
        [Header("생존자 공격 받음 이벤트")]
        public UIAttacked uiAttacked;

        //괴물
        [Header("괴물 변신 중 이벤트")]
        public UIMonsterMode uiMonsterMode;

        //관전UI
        //[Header("관전 이벤트")]
        //public UIWatching uiWatching;

        //Timer UI
        [Header("Round Timer")]
        public UITimer uiTimer;

        //게임 현황
        [Header("현재 플레이어들 현황")]
        public UINowPlayers uiNowPlayers;

        [Header("미션 달성 UI")]
        public UICompleteMission uiCompleteMission;

        [Header("전체 플레이어 미션 현황 UI")]
        public UIMissionPercent uiMissionPercent;

        //private GameObject nowPopUI;

        private void Awake()
        {
            //임시
            DataManager.Instance.LoadEventDialogData();

            this.uiNotice = GetComponentInChildren<UINotice>();
            this.uiRole = GetComponentInChildren<UIRole>();
            this.uiAttacked = GetComponentInChildren<UIAttacked>();
            this.uiMonsterMode = GetComponentInChildren<UIMonsterMode>();
            //this.uiWatching = GetComponentInChildren<UIWatching>();
            this.uiTimer = GetComponentInChildren<UITimer>();
            this.uiNowPlayers = GetComponentInChildren<UINowPlayers>();
            this.uiCompleteMission = GetComponentInChildren<UICompleteMission>();
            this.uiMissionPercent = GetComponentInChildren<UIMissionPercent>();

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
            //this.uiWatching.gameObject.SetActive(false);

            this.uiCompleteMission.gameObject.SetActive(false);
            this.uiMissionPercent.gameObject.SetActive(false);
        }
        private void Start()
        {
            //이벤트 string 전달
            EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Notice_EventUI, new EventHandler<string>((type, str) =>
            {
                if (str == "chaserChangeOff") this.uiMonsterMode.gameObject.SetActive(false);
                this.uiNotice.Init();
                Debug.Log("<color=yellow>notice</color>");
                var dialog = DataManager.Instance.GetEventDialog(str);
                Debug.LogFormat("<color=yellow>{0}</color>", dialog);
                this.uiNotice.gameObject.SetActive(true);
                //this.nowPopUI = this.uiNotice.gameObject;
                StartCoroutine(CTypingDialog(dialog, this.uiNotice.textNotice, this.uiNotice.gameObject));
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
                //this.nowPopUI = this.uiRole.gameObject;
                Debug.LogFormat("<color=yellow>{0}</color>", role);
                this.uiRole.textRole.text = roleDialog;
                StartCoroutine(CTypingDialog("주 목표 : " + questDialog, this.uiRole.textQuest, this.uiRole.gameObject));
            }));

            //공격받음 1, 2, 3단계
            EventDispatcher.instance.AddEventHandler<int>((int)NHR.EventType.eEventType.Notice_Attacked, new EventHandler<int>((type, heart) =>
            {
                //복수자가 아니면
                if (!GameDB.Instance.playerMission.isChaser)
                {
                    Debug.Log("<color=red>공격 받음</color>");

                    //UI열기
                    this.uiAttacked.OpenUI(this.uiAttacked.hearts.Length - heart - 1);

                    //목숨 줄어드는 UI
                    //this.uiAttacked.hearts[this.uiAttacked.hearts.Length - heart + 1].imageDeath.SetActive(true);

                    //기절 혹은 죽음 UI text
                    if (heart != 1)
                    {
                        this.uiAttacked.textState.text = DataManager.Instance.GetEventDialog("attacked");
                        //부활
                        Invoke("ReviveUI", 2f);
                    }
                    else
                    {
                        this.uiAttacked.textState.text = DataManager.Instance.GetEventDialog("death");
                        //Invoke("PopWatching", 2f);
                    }
                    this.uiAttacked.Close();
                }
            }));

            //부활
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Notice_Respawn, new EventHandler((type) =>
            {
                //
            }));

            //관전(죽은 후, 승리 후 다른 플레이어 기다림)
            //EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Watching_Game, new EventHandler((type) =>
            //{
            //    Debug.Log("<color=blue>관전모드</color>");
            //    this.uiWatching.gameObject.SetActive(true);
            //}));

            //괴물 변신
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Change_Monster, new EventHandler((type) =>
            {
                Debug.Log("<color=red>괴물 변신</color>");
                if (GameDB.Instance.playerMission.isChaser)
                {
                    //괴물 전용 UI 띄우기
                    this.uiMonsterMode.gameObject.SetActive(true);
                    GameDB.Instance.playerMission.photonView.RPC("CharacterChange", RpcTarget.All, "Chaser");
                }
                else
                {
                    //괴물 변신 알림
                    EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_EventUI, "chaserChangeOn");
                }
            }));

            //타이머 갱신
            EventDispatcher.instance.AddEventHandler<int>((int)NHR.EventType.eEventType.Update_Timer, new EventHandler<int>((type, time) =>
            {
                //Debug.LogFormat("<color=yellow>남은 시간 : {0}</color>", time);
                this.uiTimer.UpdateTimer(time);
            }));
            //괴물 변신 타이머 갱신
            EventDispatcher.instance.AddEventHandler<float>((int)NHR.EventType.eEventType.Update_MonsterTimer, new EventHandler<float>((type, time) =>
            {
                if (GameDB.Instance.playerMission.isChaser)
                {
                    Debug.LogFormat("<color=yellow>변신 남은 시간 : {0}</color>", time);
                    this.uiMonsterMode.UpdateTimer(time);
                }
            }));
            //미션 달성 이벤트
            EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Complete_Mission, new EventHandler<string>((type, name) =>
            {
                this.uiCompleteMission.gameObject.SetActive(true);
                this.uiCompleteMission.CompleteMission(name);
            }));
            //생존자 전체 미션 달성도 알림
            EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Notice_TotalMissionPercent, new EventHandler<string>((type, per) =>
            {
                Debug.Log("Notice TotalMission Percent");
                this.uiMissionPercent.Init();
                var dialog = string.Format(DataManager.Instance.GetEventDialog("missionPercent"), per);
                Debug.LogFormat("<color=yellow>{0}</color>", dialog);
                this.uiMissionPercent.gameObject.SetActive(true);
                //this.nowPopUI = this.uiMissionPercent.gameObject;
                StartCoroutine(CTypingDialog(dialog, this.uiMissionPercent.textNotice, this.uiMissionPercent.gameObject));
            }));
            //생존자 라운드 목표 완료 알림
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Complete_RoundMission, new EventHandler((type) =>
            {
                Debug.Log("Notice TotalMission Percent");
                this.uiMissionPercent.Init();
                var dialog = DataManager.Instance.GetEventDialog("missionPercentComplete");
                Debug.LogFormat("<color=yellow>{0}</color>", dialog);
                this.uiMissionPercent.gameObject.SetActive(true);
                //this.nowPopUI = this.uiMissionPercent.gameObject;
                StartCoroutine(CTypingDialog(dialog, this.uiMissionPercent.textNotice, this.uiMissionPercent.gameObject));
            }));
        }

        /// <summary>
        /// 부활 UI
        /// </summary>
        private void ReviveUI()
        {
            Debug.Log("<color=red>부활</color>");
            //기절 UI 없애기
            this.uiAttacked.imageDeath.gameObject.SetActive(false);

        }
        //private void PopWatching()
        //{
        //    this.uiWatching.gameObject.SetActive(true);
        //}

        //dialog 출력
        IEnumerator CTypingDialog(string dialog, TMP_Text tmp, GameObject nowPop)
        {
            foreach (var c in dialog)
            {
                tmp.text += c;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(2f);
            this.RemovePop(nowPop);
            //this.nowPopUI.gameObject.SetActive(false);
        }

        private void RemovePop(GameObject nowPop)
        {
            nowPop.SetActive(false);
        }
    }
}
