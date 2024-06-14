using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace NHR
{
    public class TutorialSceneManagerTest : MonoBehaviour
    {
        public UITutorialPlayer uiTutorialPlayer;

        //index 0�� : �޼� dialog
        public int totalIndex; //0���� ������ �� �ε��� ��
        private int currentIndex;   //���� �ε���

        //����Ʈ ������Ʈ �ε���
        public TutorialQuestObjectManager questObjectManager;
        private int nowQuestIndex = 0;

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
        }

        private IEnumerator Start()
        {
            //Ʃ�丮�� ����Ʈ Ŭ���� �̺�Ʈ ����
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Clear_TutorialQuest, new EventHandler((type) =>
            {
                Debug.Log("Clear_TutorialQuest");
                this.isClearQuest = true;
                this.uiTutorialPlayer.textDialog.text = "";
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
                        this.questObjectManager.questObjects[this.nowQuestIndex].gameObject.SetActive(true);
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
            foreach (var c in dialog)
            {
                this.uiTutorialPlayer.textDialog.text += c;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1.5f);
            Debug.Log("end");
            this.currentIndex++;
            this.isDone = true;
        }
    }

}
