using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeongMin
{
    public class ItemObjectPosition : MonoBehaviour
    {
        public enum SceneValue
        {
            tutorial,
            lobby,
            inGame
        }
        [Header("���� �°� ����")]
        public SceneValue sceneValue;
        public enum CharacterValeu
        {
            human,
            monster
        }
        [Header("������ �������� �����ϱ� ")]
        public CharacterValeu characterValeu = CharacterValeu.human;
        private void Start()
        {
            // �� ���� �Ŵ������� �ڽ��� objectPosition�� �˸���
            //if (sceneValue == SceneValue.tutorial)
            //    //GameManager.Instance.tutorialSceneManager.tutorialObjectPositionList.Add(this.transform);
            if (sceneValue == SceneValue.lobby)
                GameManager.Instance.lobbySceneManager.lobbyItemPositionList.Add(this.transform);
            else if (sceneValue == SceneValue.inGame)
            {
                GameManager.Instance.inGameMapManager.inGameItemPositionList.Add(this.transform);
            }
        }

    }
}