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
        [Header("���� �̺�Ʈ")]
        public UIWatching uiWatching;

        //Timer UI
        [Header("Round Timer")]
        public UITimer uiTimer;

        //���� ��Ȳ
        [Header("���� �÷��̾�� ��Ȳ")]
        public UINowPlayers uiNowPlayers;

        private GameObject nowPopUI;

        private void Awake()
        {
            //�ӽ�
            DataManager.Instance.LoadEventDialogData();

            this.uiNotice = FindObjectOfType<UINotice>();
            this.uiRole = FindObjectOfType<UIRole>();
            this.uiAttacked = FindObjectOfType<UIAttacked>();
            this.uiMonsterMode = FindObjectOfType<UIMonsterMode>();
            this.uiWatching = FindObjectOfType<UIWatching>();
            this.uiTimer = FindObjectOfType<UITimer>();
            this.uiNowPlayers = FindObjectOfType<UINowPlayers>();

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
            //�̺�Ʈ string ����
            EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Notice_EventUI, new EventHandler<string>((type, str) =>
            {
                if (str == "chaserChangeOff") this.uiMonsterMode.gameObject.SetActive(false);
                this.uiNotice.Init();
                Debug.Log("<color=yellow>notice</color>");
                var dialog = DataManager.Instance.GetEventDialog(str);
                Debug.LogFormat("<color=yellow>{0}</color>", dialog);
                this.uiNotice.gameObject.SetActive(true);
                this.nowPopUI = this.uiNotice.gameObject;
                StartCoroutine(CTypingDialog(dialog, this.uiNotice.textNotice));
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
                this.nowPopUI = this.uiRole.gameObject;
                Debug.LogFormat("<color=yellow>{0}</color>", role);
                this.uiRole.textRole.text = roleDialog;
                StartCoroutine(CTypingDialog("�� ��ǥ : " + questDialog, this.uiRole.textQuest));
            }));

            //���ݹ��� 1, 2, 3�ܰ�
            EventDispatcher.instance.AddEventHandler<int>((int)NHR.EventType.eEventType.Notice_Attacked, new EventHandler<int>((type, heart) =>
            {
                //�����ڰ� �ƴϸ�
                if (!GameDB.Instance.playerMission.isChaser)
                {
                    Debug.Log("<color=red>���� ����</color>");

                    //UI����
                    this.uiAttacked.OpenUI(heart);

                    //��� �پ��� UI
                    this.uiAttacked.hearts[3 - heart].imageDeath.SetActive(true);

                    //���� Ȥ�� ���� UI text
                    if (heart != 1)
                    {
                        this.uiAttacked.textState.text = DataManager.Instance.GetEventDialog("attacked");
                        //��Ȱ
                        Invoke("ReviveUI", 2f);
                    }
                    else
                    {
                        this.uiAttacked.textState.text = DataManager.Instance.GetEventDialog("death");
                        Invoke("PopWatching", 2f);
                    }
                    this.uiAttacked.Close();
                }
            }));

            //��Ȱ
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Notice_Respawn, new EventHandler((type) =>
            {
                //
            }));

            //����(���� ��, �¸� �� �ٸ� �÷��̾� ��ٸ�)
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Watching_Game, new EventHandler((type) =>
            {
                Debug.Log("<color=blue>�������</color>");
                this.uiWatching.gameObject.SetActive(true);
            }));

            //���� ����
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Change_Monster, new EventHandler((type) =>
            {
                Debug.Log("<color=red>���� ����</color>");
                if (GameDB.Instance.playerMission.isChaser)
                {
                    //���� ���� UI ����
                    this.uiMonsterMode.gameObject.SetActive(true);
                }
                else
                {
                    //���� ���� �˸�
                    EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_EventUI, "chaserChangeOn");
                }
            }));

            //Ÿ�̸� ����
            EventDispatcher.instance.AddEventHandler<int>((int)NHR.EventType.eEventType.Update_Timer, new EventHandler<int>((type, time) =>
            {
                Debug.LogFormat("<color=yellow>���� �ð� : {0}</color>", time);
                this.uiTimer.UpdateTimer(time);
            }));
            //���� ���� Ÿ�̸� ����
            EventDispatcher.instance.AddEventHandler<float>((int)NHR.EventType.eEventType.Update_MonsterTimer, new EventHandler<float>((type, time) =>
            {
                if (GameDB.Instance.playerMission.isChaser)
                {
                    Debug.LogFormat("<color=yellow>���� ���� �ð� : {0}</color>", time);
                    this.uiMonsterMode.UpdateTimer(time);
                }
            }));
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
        private void PopWatching()
        {
            this.uiWatching.gameObject.SetActive(true);
        }

        //dialog ���
        IEnumerator CTypingDialog(string dialog, TMP_Text tmp)
        {
            foreach (var c in dialog)
            {
                tmp.text += c;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(2f);
            this.nowPopUI.gameObject.SetActive(false);
        }
    }
}
