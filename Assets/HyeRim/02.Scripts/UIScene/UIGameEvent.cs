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
        [Header("�ȳ� �˾� �̺�Ʈ")]
        public UINotice uiNotice;

        [Header("���� ���� �̺�Ʈ")]
        public UIRole uiRole;

        //������
        [Header("������ ���� ���� �̺�Ʈ")]
        public UIAttacked uiAttacked;

        //����
        [Header("���� ���� �� �̺�Ʈ")]
        public UIMonsterMode uiMonsterMode;

        //����UI
        //[Header("���� �̺�Ʈ")]
        //public UIWatching uiWatching;

        //Timer UI
        [Header("Round Timer")]
        public UITimer uiTimer;

        //���� ��Ȳ
        [Header("���� �÷��̾�� ��Ȳ")]
        public UINowPlayers uiNowPlayers;

        [Header("�̼� �޼� UI")]
        public UICompleteMission uiCompleteMission;

        [Header("��ü �÷��̾� �̼� ��Ȳ UI")]
        public UIMissionPercent uiMissionPercent;

        [Header("���� ���� ����")]
        public GameObject attackSound;

        //[Header("���� UI")]
        //public UIDeath uiDeath;

        //[Header("���� ��� UI")]
        //public UIGameOver uiGameOver;

        //private GameObject nowPopUI;

        private void Awake()
        {
            //�ӽ�
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
            //�̺�Ʈ string ����
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
            //���� ����
            EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Notice_Role, new EventHandler<string>((type, role) =>
            {
                this.uiRole.Init();
                Debug.Log("<color=yellow>���� ���� �Ϸ�</color>");
                //role�� Runner�� Chaser
                var roleDialog = DataManager.Instance.GetEventDialog("role" + role);
                var questDialog = DataManager.Instance.GetEventDialog("quest" + role);

                this.uiRole.gameObject.SetActive(true);
                //this.nowPopUI = this.uiRole.gameObject;
                Debug.LogFormat("<color=yellow>{0}</color>", role);
                this.uiRole.textRole.text = roleDialog;
                StartCoroutine(CTypingDialog("�� ��ǥ : " + questDialog, this.uiRole.textQuest, this.uiRole.gameObject));
            }));

            //���ݹ��� 1, 2, 3�ܰ�
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Notice_Attacked, new EventHandler((type) =>
            {
                //����
                this.attackSound.gameObject.SetActive(true);
                //�����ڰ� �ƴϸ�
                if (!GameDB.Instance.playerMission.isChaser)
                {
                    Debug.Log("<color=red>���� ����</color>");

                    //UI����
                    this.uiAttacked.OpenUI();

                    //��� �پ��� UI
                    //this.uiAttacked.hearts[this.uiAttacked.hearts.Length - heart + 1].imageDeath.SetActive(true);

                    //���� Ȥ�� ���� UI text
                    if (GameManager.Instance.playerManager.heart > 0)
                    {
                        this.uiAttacked.textState.text = DataManager.Instance.GetEventDialog("attacked");
                        //��Ȱ
                        Invoke("ReviveUI", 2f);
                    }
                    //������
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
            //    Debug.Log("<color=yellow>���� ����, ��� �˸�</color>");
            //    this.uiGameOver.gameObject.SetActive(true);
            //    //this.uiGameOver.IsWin(GameDB.Instance.isWin);
            //}));

            //��Ȱ
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Notice_Respawn, new EventHandler((type) =>
            {
                //
            }));

            //����(���� ��, �¸� �� �ٸ� �÷��̾� ��ٸ�)
            //EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Watching_Game, new EventHandler((type) =>
            //{
            //    Debug.Log("<color=blue>�������</color>");
            //    this.uiWatching.gameObject.SetActive(true);
            //}));

            //���� ����
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Change_Monster, new EventHandler((type) =>
            {
                Debug.Log("<color=red>���� ����</color>");
                if (GameDB.Instance.playerMission.isChaser)
                {
                    //���� ���� UI ����
                    this.uiMonsterMode.gameObject.SetActive(true);
                    GameDB.Instance.playerMission.photonView.RPC("CharacterChange", RpcTarget.All, "Chaser");
                }
            }));

            //Ÿ�̸� ����
            EventDispatcher.instance.AddEventHandler<int>((int)NHR.EventType.eEventType.Update_Timer, new EventHandler<int>((type, time) =>
            {
                //Debug.LogFormat("<color=yellow>���� �ð� : {0}</color>", time);
                this.uiTimer.UpdateTimer(time);
            }));
            //���� ���� Ÿ�̸� ����
            //EventDispatcher.instance.AddEventHandler<float>((int)NHR.EventType.eEventType.Update_MonsterTimer, new EventHandler<float>((type, time) =>
            //{
            //    if (GameDB.Instance.playerMission.isChaser)
            //    {
            //        Debug.LogFormat("<color=yellow>���� ���� �ð� : {0}</color>", time);
            //        this.uiMonsterMode.UpdateTimer(time);
            //    }
            //}));
            //�̼� �޼� �̺�Ʈ
            EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Complete_Mission, new EventHandler<string>((type, name) =>
            {
                Debug.Log("<color=green>Complete_Mission �̺�Ʈ</color>");
                this.uiCompleteMission.gameObject.SetActive(true);
                this.uiCompleteMission.CompleteMission(name);
            }));

            int missionCount = 0;
            //������ ��ü �̼� �޼��� �˸�
            EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Notice_TotalMissionPercent, new EventHandler<string>((type, per) =>
            {
                missionCount++;
                Debug.Log("Notice TotalMission Percent");
                if (missionCount == 2 && GameDB.Instance.playerMission.isChaser)  //������
                {
                    Debug.Log("<color=green>���� �����ڰ� �����ڴ� �̼� 2���� �Ϸ�����</color>");
                    string str = "<color=yellow>Ż�� ���谡 ��Ÿ�����ϴ�!\n����� ���ϴ� �����ڸ� �����ϼ���!</color>";
                    this.uiMissionPercent.gameObject.SetActive(true);
                    this.uiMissionPercent.textNotice.text = str;
                    //StartCoroutine(CTypingDialog(str, this.uiMissionPercent.textNotice, this.uiMissionPercent.gameObject));
                }
                if(!GameDB.Instance.playerMission.isChaser) //������
                {
                    this.uiMissionPercent.Init();
                    string str = "<color=yellow>Ż�� ���� ����! ���踦 ��� Ż�ⱸ�� ���ϼ���\n</color>";
                    if (GameDB.Instance.playerMission.runnerMissionClearCount == 2) this.uiMissionPercent.textNotice.text = str;
                    var dialog = string.Format(DataManager.Instance.GetEventDialog("missionPercent"), per);
                    Debug.LogFormat("<color=yellow>{0}</color>", dialog);
                    this.uiMissionPercent.gameObject.SetActive(true);
                    StartCoroutine(CTypingDialog(dialog, this.uiMissionPercent.textNotice, this.uiMissionPercent.gameObject));
                }
            }));
            //Ż�ⱸ ���� ����
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Get_Final_Key, new EventHandler((type) =>
            {
                Debug.Log("Get_Final_Key");
                this.uiMissionPercent.Init();
                string str = "Ż�� ���� ȹ��!\n���踦 ��� Ż�ⱸ�� ���ϼ���!";
                if (GameDB.Instance.playerMission.isChaser) str = "�����ڰ� Ż�� ���踦 ȹ���߽��ϴ�!\nŻ�ⱸ�� ���� ��������!";
                this.uiMissionPercent.gameObject.SetActive(true);
                this.uiMissionPercent.Init();
                StartCoroutine(CTypingDialog(str, this.uiMissionPercent.textNotice, this.uiMissionPercent.gameObject));
            }));

            int attackCount = 0;
            //������ ���� ����
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Attack, new EventHandler((type) =>
            {
                attackCount++;
                Debug.Log("���� ����");
                if (attackCount <= 2&&GameDB.Instance.playerMission.isChaser)
                {
                    this.uiMissionPercent.Init();
                    string str = "���� ����!\n������ ��ġ�� �̵��˴ϴ�.";
                    this.uiMissionPercent.gameObject.SetActive(true);
                    StartCoroutine(CTypingDialog(str, this.uiMissionPercent.textNotice, this.uiMissionPercent.gameObject));
                }
            }));
            //������ ���� ��ǥ �Ϸ� �˸�
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
        /// ��Ȱ UI
        /// </summary>
        private void ReviveUI()
        {
            Debug.Log("<color=red>��Ȱ</color>");
            //���� UI ���ֱ�
            this.uiAttacked.imageDeath.gameObject.SetActive(false);

        }
        //private void PopWatching()
        //{
        //    this.uiWatching.gameObject.SetActive(true);
        //}

        //dialog ���
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
