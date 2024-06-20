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

        [Header("캐릭터 커스텀")]
        public Character[] characters;
        public Character nowCharacter;

        public string nowColorName;

        public SmartWatchCustomInteractable watch;

        public int nowCharacterID;
        public static PlayerController localPlayer;
        private void Awake()
        {
            GameDB.Instance.playerController = this;

            if (this.watch == null) this.watch = GetComponentInChildren<SmartWatchCustomInteractable>();

            this.nowCharacterID = InfoManager.Instance.PlayerInfo.nowCharacterId;
            this.nowColorName = InfoManager.Instance.PlayerInfo.nowClothesColorName;
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

            var mat = this.nowCharacter.material;
            Debug.LogFormat("<color=yellow>character : {0}, texture : {1}</color>", this.nowCharacterID, this.nowColorName);
            mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + this.nowCharacterID + this.nowColorName);

        }
        public void UpdateCharacter(int id, string colorName)
        {
            Debug.Log("UpdateCharacter");
            photonView.RPC("UpdateCharacterRPC", RpcTarget.OthersBuffered, id, colorName);
            this.ApplyCharacter(id, colorName);
        }

        [PunRPC]
        public void UpdateCharacterRPC(int id, string colorName)
        {
            Debug.Log("UpdateCharacterRPC");

            this.nowCharacterID = id;
            this.nowColorName = colorName;
            this.ApplyCharacter(id, colorName);
        }

        public void ApplyCharacter(int id, string colorName)
        {
            if(this.nowCharacter != null)
            {
                this.nowCharacter.gameObject.SetActive(false);
            }
            this.nowCharacter = this.characters[id];
            this.nowCharacterID = id;
            this.nowColorName = colorName;

            if (this.nowCharacter != null)
            {
                this.nowCharacter.gameObject.SetActive(true);
                var mat = this.nowCharacter.material;
                Debug.LogFormat("<color=yellow>character : {0}, texture : {1}</color>", this.nowCharacterID, this.nowColorName);
                mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + this.nowCharacterID + this.nowColorName);
            }
            SeongMin.GameDB.Instance.playerMission.currentRunnerCharacrer = this.characters[id];
        }


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            //if (stream.IsWriting)   //isMine인 경우
            //{
            //    stream.SendNext(this.nowCharacterID);
            //}
            //else this.nowCharacterID = (int)stream.ReceiveNext();
        }
    }

}
