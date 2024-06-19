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
        protected void Start()
        {
            if (sceneValue == SceneValue.tutorial)
                GameManager.Instance.tutorialSceneManager.tutorialObjectList.Add(this.gameObject);
            else if (sceneValue == SceneValue.lobby)
                GameManager.Instance.lobbySceneManager.lobbyItemList.Add(this.gameObject);
            else if (sceneValue == SceneValue.inGame)
            {
                if (itemValue == ItemValue.solo)
                {
                    if (charactorValue == CharactorValue.runner)
                        GameManager.Instance.inGameMapManager.inGameRunnerItemList.Add(this.gameObject);
                    else
                        GameManager.Instance.inGameMapManager.inGameChaserItemList.Add(this.gameObject);
                }
                else
                {
                    GameManager.Instance.inGameMapManager.inGameTeamPlayItemList.Add(this.gameObject);
                }
            }       
        }

    }
}