using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static NHR.App;

namespace NHR
{
    public class CharacterCustomManager : MonoBehaviour
    {
        [SerializeField]
        private UICharacterCustomManager subjectUI;

        [SerializeField]
        private CustomPlayer observerCharacter;

        private void Start()
        {
            //��ü�� ������ ���
            this.subjectUI.ResisterObserver(this.observerCharacter);
            this.subjectUI.UpdateObservers();

            //��ư �̺�Ʈ
            this.subjectUI.buttonExit.onClick.AddListener(() =>
            {
                Debug.Log("buttonExit clicked");

                //���õ� ĳ���� ���� ����
                InfoManager.Instance.EditPlayerInfo
                (this.subjectUI.selectedCharacter.characterNum, this.subjectUI.selectedClothesColor.index, this.subjectUI.selectedClothesColor.textureName);

                //Exit ������ Ÿ��Ʋ������
                EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Title);
                //SceneManager.LoadScene("TitleScene");
            });

        }
    }

}
