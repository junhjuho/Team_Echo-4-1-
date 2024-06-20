using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NHR
{
    public class PlayerController : MonoBehaviourPunCallbacks//, IPunObservable
    {

        [Header("ĳ���� Ŀ����")]
        public Character[] characters;

        public SmartWatchCustomInteractable watch;

        public int nowCharacterID;
        private void Awake()
        {
            GameDB.Instance.playerController = this;

            if (this.watch == null) this.watch = GetComponentInChildren<SmartWatchCustomInteractable>();

            this.nowCharacterID = InfoManager.Instance.PlayerInfo.nowCharacterId;
        }
        private void Start()
        {
            this.nowCharacterID = InfoManager.Instance.PlayerInfo.nowCharacterId;

            //foreach (var character in this.characters)
            //{
            //    character.gameObject.SetActive(false);
            //}
            this.characters[nowCharacterID].gameObject.SetActive(true);
        }
        public void UpdateCharacter(int id)
        {
            this.nowCharacterID = id;
            this.characters[nowCharacterID].gameObject.SetActive(true);
        }

        //private void Update()
        //{
        //    if (!photonView.IsMine) return;
        //    this.nowCharacterID = InfoManager.Instance.PlayerInfo.nowCharacterId;

        //    //foreach (var character in this.characters)
        //    //{
        //    //    character.gameObject.SetActive(false);
        //    //}
        //    this.characters[nowCharacterID].gameObject.SetActive(true);
        //    Debug.Log(this.nowCharacterID);
        //}
        //private IEnumerator Start()
        //{
        //    // ���� Ŭ���̾�Ʈ�� ��Ʈ��ũ�� ����ɶ����� ��޸���
        //    yield return new WaitUntil(() => PhotonNetwork.IsConnected);

        //    //ĳ���� Ŀ���� ����
        //    var id = 0;
        //    this.photonView.RPC("ApplyCustom", RpcTarget.AllBufferedViaServer, id);
        //}

        //[PunRPC]
        //public void ApplyCustom(int characterID)
        //{
        //    //�� ���� �ƴϸ� return
        //    if (!photonView.IsMine) return;
        //    if (this.photonView.IsMine)
        //    {
        //        //int id = InfoManager.Instance.PlayerInfo.nowCharacterId;
        //        //string colorName = InfoManager.Instance.PlayerInfo.nowClothesColorName;
        //        Debug.LogFormat("<color=yellow>Set id {0}</color>", characterID);
        //        foreach (var character in this.characters)
        //        {
        //            character.gameObject.SetActive(false);
        //        }

        //        this.characters[characterID].gameObject.SetActive(true);
        //        SeongMin.GameDB.Instance.playerMission.currentRunnerCharacrer = this.characters[characterID];
        //        //var mat = this.characters[characterID].material;
        //        //Debug.LogFormat("<color=yellow>character : {0}, texture : {1}</color>", characterID, colorName);
        //        //mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + id + colorName);

        //    }

        //}

        //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        //{
        //    if (stream.IsWriting)   //isMine�� ���
        //    {
        //        stream.SendNext(this.nowCharacterID);
        //    }
        //    else this.nowCharacterID = (int)stream.ReceiveNext();
        //}
    }

}
