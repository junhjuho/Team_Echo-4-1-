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

        [Header("공격 받은 사운드")]
        public GameObject attackSound;

        //[Header("죽음 UI")]
        //public UIDeath uiDeath;

        //[Header("게임 결과 UI")]
        //public UIGameOver uiGameOver;

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
            //this.uiDeath = GetComponentInChildren<UIDeath>();
            //this.uiGameOver = GetComponentInChildren<UIGameOver>();

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

            //this.uiDeath.gameObject.SetActive(false);
            //this.uiGameOver.gameObject.SetActive(false);
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
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Notice_Attacked, new EventHandler((type) =>
            {
                //사운드
                this.attackSound.gameObject.SetActive(true);
                //복수자가 아니면
                if (!GameDB.Instance.playerMission.isChaser)
                {
                    Debug.Log("<color=red>공격 받음</color>");

                    //UI열기
                    this.uiAttacked.OpenUI();

                    //목숨 줄어드는 UI
                    //this.uiAttacked.hearts[this.uiAttacked.hearts.Length - heart + 1].imageDeath.SetActive(true);

                    //기절 혹은 죽음 UI text
                    if (GameManager.Instance.playerManager.heart > 0)
                    {
                        this.uiAttacked.textState.text = DataManager.Instance.GetEventDialog("attacked");
                        //부활
                        Invoke("ReviveUI", 2f);
                    }
                    //죽으면
                    if (GameManager.Instance.playerManager.heart <= 0) 
                    {
                        //this.uiDeath.gameObject.SetActive(true);
                        GameDB.Instance.isWin = false;
                        EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Notice_Result);
                    }
                    this.uiAttacked.Close();
                }
            }));

            //EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Notice_Result, new EventHandler((type) =>
            //{
            //    Debug.Log("<color=yellow>게임 종료, 결과 알림</color>");
            //    this.uiGameOver.gameObject.SetActive(true);
            //    //this.uiGameOver.IsWin(GameDB.Instance.isWin);
            //}));

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
            }));

            //타이머 갱신
            EventDispatcher.instance.AddEventHandler<int>((int)NHR.EventType.eEventType.Update_Timer, new EventHandler<int>((type, time) =>
            {
                //Debug.LogFormat("<color=yellow>남은 시간 : {0}</color>", time);
                this.uiTimer.UpdateTimer(time);
            }));
            //괴물 변신 타이머 갱신
            //EventDispatcher.instance.AddEventHandler<float>((int)NHR.EventType.eEventType.Update_MonsterTimer, new EventHandler<float>((type, time) =>
            //{
            //    if (GameDB.Instance.playerMission.isChaser)
            //    {
            //        Debug.LogFormat("<color=yellow>변신 남은 시간 : {0}</color>", time);
            //        this.uiMonsterMode.UpdateTimer(time);
            //    }
            //}));
            //미션 달성 이벤트
            EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Complete_Mission, new EventHandler<string>((type, name) =>
            {
                Debug.Log("<color=green>Complete_Mission 이벤트</color>");
                this.uiCompleteMission.gameObject.SetActive(true);
                this.uiCompleteMission.CompleteMission(name);
            }));

            int missionCount = 0;
            //생존자 전체 미션 달성도 알림
            EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Notice_TotalMissionPercent, new EventHandler<string>((type, per) =>
            {
                missionCount++;
                Debug.Log("Notice TotalMission Percent");
                if (missionCount == 2 && GameDB.Instance.playerMission.isChaser)  //복수자
                {
                    Debug.Log("<color=green>나는 복수자고 생존자는 미션 2개를 완료했지</color>");
                    string str = "<color=yellow>탈출 열쇠가 나타났습니다!\n열쇠로 향하는 생존자를 방해하세요!</color>";
                    this.uiMissionPercent.gameObject.SetActive(true);
                    this.uiMissionPercent.textNotice.text = str;
                    //StartCoroutine(CTypingDialog(str, this.uiMissionPercent.textNotice, this.uiMissionPercent.gameObject));
                }
                if(!GameDB.Instance.playerMission.isChaser) //생존자
                {
                    this.uiMissionPercent.Init();
                    string str = "<color=yellow>탈출 열쇠 등장! 열쇠를 얻어 탈출구로 향하세요\n</color>";
                    if (GameDB.Instance.playerMission.runnerMissionClearCount == 2) this.uiMissionPercent.textNotice.text = str;
                    var dialog = string.Format(DataManager.Instance.GetEventDialog("missionPercent"), per);
                    Debug.LogFormat("<color=yellow>{0}</color>", dialog);
                    this.uiMissionPercent.gameObject.SetActive(true);
                    StartCoroutine(CTypingDialog(dialog, this.uiMissionPercent.textNotice, this.uiMissionPercent.gameObject));
                }
            }));
            //탈출구 열쇠 얻음
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Get_Final_Key, new EventHandler((type) =>
            {
                Debug.Log("Get_Final_Key");
                this.uiMissionPercent.Init();
                string str = "탈출 열쇠 획득!\n열쇠를 잡고 탈출구로 향하세요!";
                if (GameDB.Instance.playerMission.isChaser) str = "생존자가 탈출 열쇠를 획득했습니다!\n탈출구로 향해 막으세요!";
                this.uiMissionPercent.gameObject.SetActive(true);
                this.uiMissionPercent.Init();
                StartCoroutine(CTypingDialog(str, this.uiMissionPercent.textNotice, this.uiMissionPercent.gameObject));
            }));

            int attackCount = 0;
            //복수자 공격 성공
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Attack, new EventHandler((type) =>
            {
                attackCount++;
                Debug.Log("공격 성공");
                if (attackCount <= 2&&GameDB.Instance.playerMission.isChaser)
                {
                    this.uiMissionPercent.Init();
                    string str = "공격 성공!\n랜덤한 위치로 이동됩니다.";
                    this.uiMissionPercent.gameObject.SetActive(true);
                    StartCoroutine(CTypingDialog(str, this.uiMissionPercent.textNotice, this.uiMissionPercent.gameObject));
                }
            }));
            //생존자 라운드 목표 완료 알림
            //EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Complete_RoundMission, new EventHandler((type) =>
            //{
            //    if (!GameDB.Instance.playerMission.isChaser)
            //    {
            //        Debug.Log("Notice TotalMission Percent");
            //        this.uiMissionPercent.Init();
            //        var dialog = DataManager.Instance.GetEventDialog("missionPercentComplete");
            //        Debug.LogFormat("<color=yellow>{0}</color>", dialog);
            //        this.uiMissionPercent.gameObject.SetActive(true);
            //        //this.nowPopUI = this.uiMissionPercent.gameObject;
            //        StartCoroutine(CTypingDialog(dialog, this.uiMissionPercent.textNotice, this.uiMissionPercent.gameObject));
            //    }
            //}));
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
            Debug.Log(nowPop);
            nowPop.SetActive(false);
        }
    }
}
