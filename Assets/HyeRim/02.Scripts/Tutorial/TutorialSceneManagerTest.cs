using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace NHR
{
    public class TutorialSceneManagerTest : MonoBehaviour
    {
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
            //�ε��� �� ��������
            this.totalIndex = DataManager.Instance.totalTutorialIndex;
            this.currentIndex = 1;

            this.nowQuestIndex = 0;

            //��Ʈ �������
            this.uiTutorialPlayer.textDialog.color = Color.white;
            this.uiTutorialPlayer.textDialog.fontStyle = FontStyles.Normal;

        }

        private IEnumerator Start()
        {
            //Ʃ�丮�� ����Ʈ Ŭ���� �̺�Ʈ ����
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Clear_TutorialQuest, new EventHandler((type) =>
            {
                Debug.Log("Clear_TutorialQuest");
                this.isClearQuest = true;
                this.uiTutorialPlayer.textDialog.text = "";

                var questObject = this.questObjectManager.questObjects[this.currentIndex];
                if (this.currentIndex == 5) questObject.GetComponent<TutorialQuestObjectTrigger>().isQuestDone = true;

                this.questPosArrow.SetActive(false);
                this.uiTutorialPlayer.arrowBillboard.gameObject.SetActive(false);
                this.currentIndex++;
                this.nowQuestIndex++;
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
                        this.uiTutorialPlayer.arrowBillboard.gameObject.SetActive(true);
                        this.uiTutorialPlayer.arrowBillboard.targetTf = questObj.transform;

                        Debug.LogFormat("currentIndex : {0}, removeIndex : {1}", this.currentIndex, data.removeTartgetIndex);
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

        //dialog ���
        IEnumerator CTypingDialog(string dialog)
        {
            if (!this.isClearQuest)
            {
                //����Ʈ�� �� ���̰�
                this.uiTutorialPlayer.textDialog.color = Color.yellow;
                this.uiTutorialPlayer.textDialog.fontStyle = FontStyles.Bold;
            }
            foreach (var c in dialog)
            {
                this.uiTutorialPlayer.textDialog.text += c;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1.5f);
            Debug.Log("end");
            this.isDone = true;
            this.currentIndex++;
        }
    }

}
