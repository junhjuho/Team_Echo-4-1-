using Photon.Pun;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NHR
{
    public interface ICharacterObserver
    {
        //������ ������Ʈ ĳ���͹�ȣ, �ؽ��� �̸�
        void ObserverUpdate(int characterNum, string textureName);
    }

    public class CustomPlayer : MonoBehaviour, ICharacterObserver
    {
        //���� ĳ���͹�ȣ, �ؽ��� �̸�
        public int nowCharacterNum;
        public string nowColorName;

        //public TMP_Text characterName;

        public PlayerController playerController;


        public void ObserverUpdate(int characterNum, string colorName)
        {
            Debug.LogFormat("in CustomPlayer num : {0}, texture : {1}", characterNum, colorName);
            //���޵� ���� ������Ʈ

            this.nowCharacterNum = characterNum;

            this.playerController.UpdateCharacter(characterNum);
        }
    }
}
