using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace NHR
{
    public class TutorialSceneManager : MonoBehaviour
    {
        [Header("튜토리얼 플레이어 UI")]
        public UITutorialPlayer uiTutorialPlayer;

        //index 0번 : 달성 dialog
        [Header("총 인덱스 수")]
        public int totalIndex; //0번을 제외한 총 인덱스 수

        [Header("현재 인덱스")]
        public int currentIndex;   //현재 인덱스

        //퀘스트 오브젝트 인덱스
        [Header("튜토리얼 퀘스트 Manager")]
        public TutorialQuestObjectManager questObjectManager;

        [Header("현재 퀘스트 인덱스")]
        private int nowQuestIndex = 0;

        [Header("현재 퀘스트 장소 오브젝트")]
        public GameObject questPosArrow;

        //현재 dialog 출력을 완료했는가?
        private bool isDone;
        //현재 퀘스트를 클리어했는가?
        private bool isClearQuest;
        private bool isClearTime;

        private WaitForSeconds typingCharSec = new WaitForSeconds(0.05f);
        private WaitForSeconds typingClearSec = new WaitForSeconds(1.5f);
        private void Awake()
        {
            //임시
            DataManager.Instance.LoadTutorialData();

            this.Init();
        }

        //초기화
        public void Init()
        {
            this.uiTutorialPlayer.textDialog.text = "";
            this.isDone = true;
            this.isClearQuest = true;
            //this.isClearTime = false;
            //인덱스 수 가져오기
            this.totalIndex = DataManager.Instance.totalTutorialIndex;
            this.currentIndex = 1;
            this.nowQuestIndex = 0;

            this.questPosArrow.SetActive(false);

            //폰트 원래대로
            this.FontInit();
        }

        private IEnumerator Start()
        {
            //튜토리얼 퀘스트 클리어 이벤트 정의
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Clear_TutorialQuest, new EventHandler((type) =>
            {
                //Debug.Log("Clear_TutorialQuest");
                this.isClearQuest = true;
                this.uiTutorialPlayer.textDialog.text = "";

                var questObject = this.questObjectManager.questObjects[this.nowQuestIndex];
                //트리거 퀘스트
                if (this.nowQuestIndex == 1) questPosArrow.GetComponent<TutorialQuestObjectTrigger>().isQuestDone = true;
                if (this.nowQuestIndex > 4) questObject.GetComponentInChildren<TutorialQuestObjectTrigger>().isQuestDone = true;

                Debug.LogFormat("nowIndex :{0}, nowQuestIndex : {1}", this.currentIndex, this.nowQuestIndex);

                this.questPosArrow.SetActive(false);
                //this.uiTutorialPlayer.arrowBillboard.gameObject.SetActive(false);

                //퀘스트 애니메이션 끄기
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
                    //튜토리얼 클리어 조건
                    if(this.currentIndex == this.totalIndex)
                    {
                        Debug.Log("튜토리얼 클리어");
                        break;
                    }
                    //dialog 출력
                    var data = DataManager.Instance.GetTutorialData(this.currentIndex);

                    //데이터 타입 -1이면 그냥 출력, 0, 1이면 퀘스트 완수해야 다음 출력
                    if (data.type != -1)
                    {
                        //퀘스트 조건 설정
                        this.isClearQuest = false;

                        //퀘스트 오브젝트
                        var questObj = this.questObjectManager.questObjects[this.nowQuestIndex].gameObject;
                        questObj.SetActive(true);

                        //퀘스트 장소 화살표 위치 이동
                        this.questPosArrow.SetActive(true);
                        this.questPosArrow.transform.position = 
                            new Vector3(questObj.transform.position.x, this.questPosArrow.transform.position.y, questObj.transform.position.z);

                        //퀘스트 안내 화살표 lookat
                        //this.uiTutorialPlayer.arrowBillboard.gameObject.SetActive(true);
                        //this.uiTutorialPlayer.arrowBillboard.targetTf = questObj.transform;

                        //퀘스트 애니메이션
                        this.SetHandsQuest(this.nowQuestIndex);

                        yield return null;
                    }
                    if (this.currentIndex == 2)
                    {
                        //스타트
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
                //이동
                case 0:
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("Move");
                    break;
                //달리기
                case 1:
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("Move");
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("ButtonA");
                    break;
                //손전등 잡기
                case 2:
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("Grab");
                    break;
                //손전등 Trigger
                case 3:
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("Trigger");
                    break;
                //배터리 잡기
                case 4:
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("Grab");
                    break;
                //열쇠 잡기+소켓 넣기
                case 5:
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("Grab");
                    break;
                //복수자 아이템 잡기
                case 6:
                    this.uiTutorialPlayer.tutorialHands.SetAnimation("Grab");
                    break;
            }
        }

        //dialog 출력
        IEnumerator CTypingDialog(string dialog)
        {
            string text = dialog;
            if (!this.isClearQuest)
            {
                this.uiTutorialPlayer.textDialog.text = "";
                //퀘스트면 잘 보이게
                this.uiTutorialPlayer.textDialog.color = Color.yellow;
                this.uiTutorialPlayer.textDialog.fontStyle = FontStyles.Bold;
            }
            else this.FontInit();

            //클리어 직후면 클리어 멘트 출력
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
            //폰트 원래대로
            this.uiTutorialPlayer.textDialog.color = Color.white;
            this.uiTutorialPlayer.textDialog.fontStyle = FontStyles.Normal;
        }
    }

}
