using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace NHR
{
    public class TutorialSceneManager : MonoBehaviour
    {
        [Header("Ʃ�丮�� �÷��̾� UI")]
        public UITutorialPlayer uiTutorialPlayer;

        //index 0�� : �޼� dialog
        [Header("�� �ε��� ��")]
        public int totalIndex; //0���� ������ �� �ε��� ��

        [Header("���� �ε���")]
        public int currentIndex;   //���� �ε���

        //����Ʈ ������Ʈ �ε���
        [Header("Ʃ�丮�� ����Ʈ Manager")]
        public TutorialQuestObjectManager questObjectManager;

        [Header("���� ����Ʈ �ε���")]
        private int nowQuestIndex = 0;

        [Header("���� ����Ʈ ��� ������Ʈ")]
        public GameObject questPosArrow;

        //���� dialog ����� �Ϸ��ߴ°�?
        private bool isDone;
        //���� ����Ʈ�� Ŭ�����ߴ°�?
        private bool isClearQuest;
        private bool isClearTime;

        private WaitForSeconds typingCharSec = new WaitForSeconds(0.05f);
        private WaitForSeconds typingClearSec = new WaitForSeconds(1.5f);
        private void Awake()
        {
            //�ӽ�
            DataManager.Instance.LoadTutorialData();

            this.Init();
        }

        //�ʱ�ȭ
        public void Init()
        {
            this.uiTutorialPlayer.textDialog.text = "";
            this.isDone = true;
            this.isClearQuest = true;
            //this.isClearTime = false;
            //�ε��� �� ��������
            this.totalIndex = DataManager.Instance.totalTutorialIndex;
            this.currentIndex = 1;
            this.nowQuestIndex = 0;

            this.questPosArrow.SetActive(false);

            //��Ʈ �������
            this.FontInit();
        }

        private IEnumerator Start()
        {
            //Ʃ�丮�� ����Ʈ Ŭ���� �̺�Ʈ ����
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Clear_TutorialQuest, new EventHandler((type) =>
            {
                //Debug.Log("Clear_TutorialQuest");
                this.isClearQuest = true;
                this.uiTutorialPlayer.textDialog.text = "";

                var questObject = this.questObjectManager.questObjects[this.nowQuestIndex];
                //Ʈ���� ����Ʈ
                if (this.nowQuestIndex == 1) questPosArrow.GetComponent<TutorialQuestObjectTrigger>().isQuestDone = true;
                if (this.nowQuestIndex > 4) questObject.GetComponentInChildren<TutorialQuestObjectTrigger>().isQuestDone = true;

                Debug.LogFormat("nowIndex :{0}, nowQuestIndex : {1}", this.currentIndex, this.nowQuestIndex);

                this.questPosArrow.SetActive(false);
                //this.uiTutorialPlayer.arrowBillboard.gameObject.SetActive(false);

                //����Ʈ �ִϸ��̼� ����
                this.uiTutorialPlayer.tutorialHands.Init();

                //this.currentIndex++;
                this.nowQuestIndex++;
                this.isClearTime = true;
            }));

            while (true)
            {
                yield return null;
                if (this.isDone && this.isClearQuest) 
                {
                    //Ʃ�丮�� Ŭ���� ����
                    if(this.currentIndex == this.totalIndex)
                    {
                        Debug.Log("Ʃ�丮�� Ŭ����");
                        break;
                    }
                    //dialog ���
                    var data = DataManager.Instance.GetTutorialData(this.currentIndex);

                    //������ Ÿ�� -1�̸� �׳� ���, 0, 1�̸� ����Ʈ �ϼ��ؾ� ���� ���
                    if (data.type != -1)
                    {
                        //����Ʈ ���� ����
                        this.isClearQuest = false;

                        //����Ʈ ������Ʈ
                        var questObj = this.questObjectManager.questObjects[this.nowQuestIndex].gameObject;
                        questObj.SetActive(true);

                        //����Ʈ ��� ȭ��ǥ ��ġ �̵�
                        this.questPosArrow.SetActive(true);
                        this.questPosArrow.transform.position = 
                            new Vector3(questObj.transform.position.x, this.questPosArrow.transform.position.y, questObj.transform.position.z);

                        //����Ʈ �ȳ� ȭ��ǥ lookat
                        //this.uiTutorialPlayer.arrowBillboard.gameObject.SetActive(true);
                        //this.uiTutorialPlayer.arrowBillboard.targetTf = questObj.transform;

                        //����Ʈ �ִϸ��̼�
                        this.SetHandsQuest(this.nowQuestIndex);

                        yield return null;
                    }
                    if (this.currentIndex == 2)
                    {
                        //��ŸƮ
                        this.uiTutorialPlayer.textDialog.text = "";
                    }
                    this.isDone = false;
                    StartCoroutine(CTypingDialog(data.dialog));
                }
            }
        }

        private void SetHandsQuest(int questIndex)
        {
            switch (questIndex)
            {
                //�̵�
                case 0:
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("Move");
                    break;
                //�޸���
                case 1:
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("Move");
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("ButtonA");
                    break;
                //������ ���
                case 2:
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("Grab");
                    break;
                //������ Trigger
                case 3:
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("Trigger");
                    break;
                //���͸� ���
                case 4:
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("Grab");
                    break;
                //���� ���+���� �ֱ�
                case 5:
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("Grab");
                    break;
                //������ ������ ���
                case 6:
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("Grab");
                    break;
            }
        }

        //dialog ���
        IEnumerator CTypingDialog(string dialog)
        {
            string text = dialog;
            if (!this.isClearQuest)
            {
                this.uiTutorialPlayer.textDialog.text = "";
                //����Ʈ�� �� ���̰�
                this.uiTutorialPlayer.textDialog.color = Color.yellow;
                this.uiTutorialPlayer.textDialog.fontStyle = FontStyles.Bold;
            }
            else this.FontInit();

            //Ŭ���� ���ĸ� Ŭ���� ��Ʈ ���
            if (this.isClearTime)
            {
                text = DataManager.Instance.GetTutorialData(0).dialog + "\n" + dialog;
                this.isClearTime = false;
            }

            foreach (var c in text)
            {
                this.uiTutorialPlayer.textDialog.text += c;
                yield return this.typingCharSec;
            }
            yield return this.typingClearSec;
            Debug.Log("end");
            this.isDone = true;
            this.currentIndex++;
        }

        private void FontInit()
        {
            //��Ʈ �������
            this.uiTutorialPlayer.textDialog.color = Color.white;
            this.uiTutorialPlayer.textDialog.fontStyle = FontStyles.Normal;
        }
    }

}
