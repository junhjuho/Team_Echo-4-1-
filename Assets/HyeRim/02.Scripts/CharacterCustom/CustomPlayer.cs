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
        //옵저버 업데이트 캐릭터번호, 텍스쳐 이름
        void ObserverUpdate(int characterNum, string textureName);
    }

    public class CustomPlayer : MonoBehaviour, ICharacterObserver
    {
        //현재 캐릭터번호, 텍스쳐 이름
        public int nowCharacterNum;
        public string nowColorName;

        //public TMP_Text characterName;

        public PlayerController playerController;


        public void ObserverUpdate(int characterNum, string colorName)
        {
            Debug.LogFormat("in CustomPlayer num : {0}, texture : {1}", characterNum, colorName);
            //전달된 정보 업데이트

            this.nowCharacterNum = characterNum;

            this.playerController.UpdateCharacter(characterNum);
        }
    }
}
