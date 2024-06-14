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

        //������
        public UIAttacked uiAttacked;

        //����
        public UIMonsterMode uiMonsterMode;

        //����UI
        public UIWatching uiWatching;

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
                Debug.Log("<color=red>���� ����</color>");

                //UI����
                this.uiAttacked.OpenUI(heart);

                //��� �پ��� UI
                this.uiAttacked.hearts[3 - heart].imageDeath.SetActive(true);

                //���� Ȥ�� ���� UI text
                if (heart != 1) this.uiAttacked.textState.text = DataManager.Instance.GetEventDialog("attacked");
                else
                {
                    this.uiAttacked.textState.text = DataManager.Instance.GetEventDialog("death");
                    Invoke("PopWatching", 2f);
                }
                this.uiAttacked.Close();
            }));

            //��Ȱ
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Notice_Respawn, new EventHandler((type) =>
            {
                Debug.Log("<color=red>��Ȱ</color>");
                //���� UI ���ֱ�
                this.uiAttacked.imageDeath.gameObject.SetActive(false);
            }));

            //����(���� ��, �¸� �� �ٸ� �÷��̾� ��ٸ�)
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Watching_Game, new EventHandler((type) =>
            {
                Debug.Log("<color=blue>�������</color>");

            }));
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
            //this.nowPopUI.gameObject.SetActive(false);
        }
    }
}
