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
    public class PlayerController : MonoBehaviour
    {
        private string characterKey = "characterID";
        private string colorKey = "colorName";

        [Header("캐릭터 커스텀")]
        public Character[] characters;

        public SmartWatchCustomInteractable watch;

        public PhotonView photonView;

        private void Awake()
        {
            GameDB.Instance.playerController = this;

            if (this.watch == null) this.watch = GetComponentInChildren<SmartWatchCustomInteractable>();
        }
        private IEnumerator Start()
        {
            this.photonView = GetComponent<PhotonView>();
            // 나의 클라이언트가 네트워크에 연결될때까지 기달리기
            yield return new WaitUntil(() => PhotonNetwork.IsConnected);

            //캐릭터 커스텀 설정
            this.photonView.RPC("ApplyCustom", RpcTarget.AllBuffered);
        }

        [PunRPC]
        public void ApplyCustom()
        {

            int id = InfoManager.Instance.PlayerInfo.nowCharacterId;
            string colorName = InfoManager.Instance.PlayerInfo.nowClothesColorName;
            Debug.LogFormat("<color=yellow>Set id {0}</color>", id);
            foreach (var character in this.characters)
            {
                character.gameObject.SetActive(false);
            }

            this.characters[id].gameObject.SetActive(true);
            SeongMin.GameDB.Instance.playerMission.currentRunnerCharacrer = this.characters[id];
            var mat = this.characters[id].material;
            Debug.LogFormat("<color=yellow>character : {0}, texture : {1}</color>", id, colorName);
            mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + id + colorName);

        }

    }

}
