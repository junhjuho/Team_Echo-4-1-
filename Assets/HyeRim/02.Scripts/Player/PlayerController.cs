using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TreeEditor.TextureAtlas;
using HashTable = ExitGames.Client.Photon.Hashtable;

namespace NHR
{
    public class PlayerController : MonoBehaviourPunCallbacks
    {
        private string customKey = "playerCustom";
        [Header("캐릭터 커스텀")]
        public Character[] characters;
        [Header("photonview")]
        //public PhotonView photonView;

        public SmartWatchCustomInteractable watch;

        private void Awake()
        {
            GameDB.Instance.playerController = this;
            //this.photonView = this.GetComponent<PhotonView>();

            //this.Init();
            if (this.watch == null) this.watch = GetComponentInChildren<SmartWatchCustomInteractable>();
        }
        private void Start()
        {
            if(photonView.IsMine)this.SetPlayer(InfoManager.Instance.PlayerInfo.nowCharacterId);
        }


        /// <summary>
        /// 플레이어 접속 시커스텀프로퍼티에 플레이어 캐릭터 커스텀 정보 동기화
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="textureName"></param>
        public void SetPlayer(int id)
        {
            Debug.Log("<color=white>PlayerOn SetCustomProperties</color>");
            //playerCustom["playerCustom"] = (InfoManager.Instance.PlayerInfo.nowCharacterId, InfoManager.Instance.PlayerInfo.nowClothesColorName);
            HashTable props = new HashTable()
            {
                {customKey, id }
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }
        public override void OnPlayerPropertiesUpdate(Player _player, HashTable _changedProps) // 커스텀 프로퍼티 변경시 콜백 받는 함수
        {
            //캐릭터 커스텀
            if (_changedProps.ContainsKey(customKey) && _player == photonView.Owner)
            {
                int customPlayer = (int)_changedProps[customKey];
                Debug.LogFormat("<color=white>playerCustom {0}</color>", customPlayer);
                Debug.LogFormat("<color=green>photonView.Owner {0}</color>", this.photonView.Owner.IsLocal);

                if (_player == this.photonView.Owner)
                {
                    Debug.Log("<color=white>playerCustom success</color>");
                    //GameDB.Instance.playerController.Init();
                    //GameManager.Instance.lobbySceneManager.playerController.Init();
                    Debug.LogFormat("<color=green>photonView.Owner {0}</color>", this.name);
                    this.ApplyCustom(customPlayer);
                    //UIManager.Instance.robbySceneMenu.SetPlayer(customPlayer);
                }
            }
            //if (_changedProps.ContainsKey("playerCustom"))
            //{
            //    var customPlayer = ((int, string))_changedProps["playerCustom"];

            //    if (_player == PhotonNetwork.LocalPlayer)
            //    {
            //        //GameDB.Instance.playerController.Init();
            //        GameManager.Instance.lobbySceneManager.playerController.Init();
            //    }
            //}
        }

        public void ApplyCustom(int id)
        {
            Debug.LogFormat("<color=yellow>Set id {0}</color>", id);
            foreach (var character in this.characters)
            {
                character.gameObject.SetActive(false);
            }

            this.characters[id].gameObject.SetActive(true);
        }

        //public void Init()
        //{
        //    //인포에 저장된 캐릭터 불러오기
        //    PhotonView photonview = null;
        //    if (GameManager.Instance.roundManager != null) photonview = GameManager.Instance.roundManager.GetComponent<PhotonView>();
        //    else if(GameManager.Instance.lobbySceneManager != null) photonview = GameManager.Instance.lobbySceneManager.GetComponent<PhotonView>();
        //    var playerCustom = ((int, string))photonview.Owner.CustomProperties["playerCustom"];
        //    var selectedCharacterID = playerCustom.Item1;
        //    var selectedClothesColorName = playerCustom.Item2;

        //    //캐릭터 설정
        //    //this.characters = this.GetComponentsInChildren<Character>();
        //    foreach (var character in this.characters)
        //    {
        //        character.gameObject.SetActive(false);
        //    }
        //    this.characters[selectedCharacterID].gameObject.SetActive(true);
        //    var mat = this.characters[selectedCharacterID].material;
        //    Debug.LogFormat("<color=yellow>character : {0}, texture : {1}</color>", selectedCharacterID, selectedClothesColorName);
        //    mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + selectedCharacterID + selectedClothesColorName);

        //}

        public override void OnJoinedRoom()
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (player.CustomProperties.ContainsKey(this.customKey))
                {
                    int id = (int)player.CustomProperties[this.customKey];
                    // 이 플레이어의 외형을 적용합니다.
                    if (player == PhotonNetwork.LocalPlayer)
                    {
                        ApplyCustom(id);
                    }
                }
            }
        }
    }

}
