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
        public string nowTextureName;

        public TMP_Text characterName;

        public PhotonView photonView;

        public PlayerController playerController;

        //public Character[] characters;

        private void Awake()
        {
            this.Init();
        }
        private void Init()
        {
            //this.characters = this.GetComponentsInChildren<Character>();
            //foreach (var character in this.characters)
            //{
            //    character.gameObject.SetActive(false);
            //}
            //this.characters[this.nowCharacterNum].gameObject.SetActive(true);
            //this.characterName.text = this.characters[this.nowCharacterNum].name;
        }
        //private IEnumerator Start()
        //{
        //    //.photonView = GetComponent<PhotonView>();
        //    // 나의 클라이언트가 네트워크에 연결될때까지 기달리기
        //    yield return new WaitUntil(() => PhotonNetwork.IsConnected);

        //    //this.playerController = GameDB.Instance.myPlayer.GetComponent<PlayerController>();
        //    //캐릭터 커스텀 설정
        //    //this.photonView.RPC("ApplyCustom", RpcTarget.AllBuffered);
        //}

        public void ObserverUpdate(int characterNum, string textureName)
        {
            Debug.LogFormat("in CustomPlayer num : {0}, texture : {1}", characterNum, textureName);
            //전달된 정보 업데이트

            this.nowCharacterNum = characterNum;
            this.nowTextureName = textureName;

            this.playerController.UpdateCharacter(nowCharacterNum);
            //Debug.LogFormat("<color=yellow>Set id {0}</color>", characterNum);
            //foreach (var character in this.playerController.characters)
            //{
            //    character.gameObject.SetActive(false);
            //}

            //this.playerController.characters[characterNum].gameObject.SetActive(true);
            //SeongMin.GameDB.Instance.playerMission.currentRunnerCharacrer = this.playerController.characters[characterNum];
            //this.playerController.photonView.RPC("ApplyCustom", RpcTarget.AllBuffered);

            //이전 선택과 다를 경우
            //if (this.nowCharacterNum != characterNum)
            //{
            //    //캐릭터 변신
            //    //this.characters[this.nowCharacterNum].gameObject.SetActive(false);
            //    this.nowCharacterNum = characterNum;
            //    //this.characters[this.nowCharacterNum].gameObject.SetActive(true);
            //    //this.characterName.text = this.characters[this.nowCharacterNum].name;

            //    Debug.LogFormat("<color=yellow>Set id {0}</color>", characterNum);
            //    foreach (var character in this.playerController.characters)
            //    {
            //        character.gameObject.SetActive(false);
            //    }

            //    this.playerController.characters[characterNum].gameObject.SetActive(true);
            //    SeongMin.GameDB.Instance.playerMission.currentRunnerCharacrer = this.playerController.characters[characterNum];

            //}
            //텍스쳐 바꿈
            //var mat = this.playerController.characters[characterNum].material;
            //Debug.LogFormat("<color=yellow>character : {0}, texture : {1}</color>", characterNum, textureName);
            //mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + characterNum + textureName);

            //var mat = this.characters[this.nowCharacterNum].material;
            //Debug.LogFormat("<color=yellow>material : {0}</color>", mat.name);
            ////Debug.LogFormat("<color=yellow>main texture : {0}</color>", mat.mainTexture.name);
            //mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + this.nowCharacterNum + textureName);


        }
    }
}
