using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeongMin
{
    public class ItemObject : MonoBehaviour
    {
        public enum SceneValue
        {
            tutorial,
            lobby,
            inGame
        }
        public enum CharactorValue
        {
            runner,
            chaser
        }
        public enum ItemValue
        {
            solo,
            teamPlay
        }
        [Header("각 씬에 맞게 선택하기")]
        public SceneValue sceneValue;
        [Header("복수자 전용 선택하기")]
        public CharactorValue charactorValue = CharactorValue.runner;
        [Header("협동 아이템인지 선택하기")]
        public ItemValue itemValue = ItemValue.solo;
        [Header("플레이어에게 잡혔는지 확인하기")]
        public bool isFind = false;
        [Header("파티클과 미니맵 아이콘 자동 할당되는 곳 ")]
        public GameObject fx;
        public GameObject miniMap;
        [Header("캔버스 용 트리거 체크 오브젝트")]
        public GameObject triggerObject;
        protected void Start()
        {
            miniMap = transform.parent.transform.Find("MinimapIcon").gameObject;

            if (miniMap != null&&this.gameObject.name!= "탈출구 열쇠")
                miniMap.SetActive(false);

            //if (sceneValue == SceneValue.tutorial)
            //    GameManager.Instance.tutorialSceneManager.tutorialObjectList.Add(this.gameObject.transform.parent.gameObject);
            if (sceneValue == SceneValue.lobby)
                GameManager.Instance.lobbySceneManager.lobbyItemList.Add(this.gameObject.transform.parent.gameObject);
            else if (sceneValue == SceneValue.inGame)
            {
                if (itemValue == ItemValue.solo)
                {
                    if (charactorValue == CharactorValue.runner)
                        GameManager.Instance.inGameMapManager.inGameRunnerItemList.Add(this.gameObject.transform.parent.gameObject);
                    else
                    {
                        GameManager.Instance.inGameMapManager.inGameChaserItemList.Add(this.gameObject.transform.parent.gameObject);
                        fx = transform.Find("FX").gameObject;
                        fx.SetActive(false);
                    }
                }
                else
                {
                    GameManager.Instance.inGameMapManager.inGameTeamPlayItemList.Add(this.gameObject.transform.parent.gameObject);
                }
            }       
        }

    }
}