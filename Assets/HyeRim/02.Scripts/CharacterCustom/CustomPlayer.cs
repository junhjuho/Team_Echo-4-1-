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
        //    // ���� Ŭ���̾�Ʈ�� ��Ʈ��ũ�� ����ɶ����� ��޸���
        //    yield return new WaitUntil(() => PhotonNetwork.IsConnected);

        //    //this.playerController = GameDB.Instance.myPlayer.GetComponent<PlayerController>();
        //    //ĳ���� Ŀ���� ����
        //    //this.photonView.RPC("ApplyCustom", RpcTarget.AllBuffered);
        //}

        public void ObserverUpdate(int characterNum, string textureName)
        {
            Debug.LogFormat("in CustomPlayer num : {0}, texture : {1}", characterNum, textureName);
            //���޵� ���� ������Ʈ

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

            //���� ���ð� �ٸ� ���
            //if (this.nowCharacterNum != characterNum)
            //{
            //    //ĳ���� ����
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
            //�ؽ��� �ٲ�
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
