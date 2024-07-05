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
        [Header("�� ���� �°� �����ϱ�")]
        public SceneValue sceneValue;
        [Header("������ ���� �����ϱ�")]
        public CharactorValue charactorValue = CharactorValue.runner;
        [Header("���� ���������� �����ϱ�")]
        public ItemValue itemValue = ItemValue.solo;
        [Header("�÷��̾�� �������� Ȯ���ϱ�")]
        public bool isFind = false;
        [Header("��ƼŬ�� �̴ϸ� ������ �ڵ� �Ҵ�Ǵ� �� ")]
        public GameObject fx;
        public GameObject miniMap;
        [Header("ĵ���� �� Ʈ���� üũ ������Ʈ")]
        public GameObject triggerObject;
        protected void Start()
        {
            miniMap = transform.parent.transform.Find("MinimapIcon").gameObject;

            if (miniMap != null&&this.gameObject.name!= "Ż�ⱸ ����")
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