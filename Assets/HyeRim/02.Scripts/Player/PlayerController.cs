using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace NHR
{
    public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
    {

        [Header("ĳ���� Ŀ����")]
        public Character[] characters;
        public Character nowCharacter;

        public SmartWatchCustomInteractable watch;

        public int nowCharacterID;
        public static PlayerController localPlayer;
        private void Awake()
        {
            GameDB.Instance.playerController = this;

            if (this.watch == null) this.watch = GetComponentInChildren<SmartWatchCustomInteractable>();

            this.nowCharacterID = InfoManager.Instance.PlayerInfo.nowCharacterId;
        }
        private void Start()
        {
            if(photonView.IsMine)
            {
                localPlayer = this;
                Debug.LogFormat("<color=green>PlayerID : {0}</color>", InfoManager.Instance.PlayerInfo.nowCharacterId);
            }
            this.nowCharacterID = InfoManager.Instance.PlayerInfo.nowCharacterId;

            this.characters[nowCharacterID].gameObject.SetActive(true);
            this.nowCharacter = this.characters[nowCharacterID];
            SeongMin.GameDB.Instance.playerMission.currentRunnerCharacrer = this.characters[nowCharacterID];
        }
        public void UpdateCharacter(int id)
        {
            Debug.Log("UpdateCharacter");
            photonView.RPC("UpdateCharacterRPC", RpcTarget.OthersBuffered, id);
            this.ApplyCharacter(id);
        }

        [PunRPC]
        public void UpdateCharacterRPC(int id)
        {
            Debug.Log("UpdateCharacterRPC");

            this.nowCharacterID = id;
            this.ApplyCharacter(id);
        }

        public void ApplyCharacter(int id)
        {
            if(this.nowCharacter != null)
            {
                this.nowCharacter.gameObject.SetActive(false);
            }
            this.nowCharacter = this.characters[id];
            this.nowCharacterID = id;

            if (this.nowCharacter != null)
            {
                this.nowCharacter.gameObject.SetActive(true);
            }
            SeongMin.GameDB.Instance.playerMission.currentRunnerCharacrer = this.characters[id];
        }


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            //if (stream.IsWriting)   //isMine�� ���
            //{
            //    stream.SendNext(this.nowCharacterID);
            //}
            //else this.nowCharacterID = (int)stream.ReceiveNext();
        }
    }

}
