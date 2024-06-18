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
        private string characterKey = "characterID";
        private string colorKey = "colorName";

        [Header("캐릭터 커스텀")]
        public Character[] characters;

        public SmartWatchCustomInteractable watch;

        private void Awake()
        {
            GameDB.Instance.playerController = this;

            if (this.watch == null) this.watch = GetComponentInChildren<SmartWatchCustomInteractable>();
        }
        private void Start()
        {
            if(photonView.IsMine)this.SetPlayer(InfoManager.Instance.PlayerInfo.nowCharacterId, InfoManager.Instance.PlayerInfo.nowClothesColorName);
        }


        public void SetPlayer(int id, string colorName)
        {
            Debug.Log("<color=white>PlayerOn SetCustomProperties</color>");
            HashTable props = new HashTable()
            {
                {characterKey, id },
                {colorKey, colorName}
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }
        public override void OnPlayerPropertiesUpdate(Player _player, HashTable _changedProps) // 커스텀 프로퍼티 변경시 콜백 받는 함수
        {
            //캐릭터 커스텀
            if (_changedProps.ContainsKey(characterKey) && _player == photonView.Owner)
            {
                int customPlayer = (int)_changedProps[characterKey];
                string customColor = (string)_changedProps[colorKey];
                Debug.LogFormat("<color=white>playerCustom {0}</color>", customPlayer);
                Debug.LogFormat("<color=white>customColor {0}</color>", customColor);
                Debug.LogFormat("<color=green>photonView.Owner {0}</color>", this.photonView.Owner.IsLocal);

                if (_player == this.photonView.Owner)
                {
                    Debug.Log("<color=white>playerCustom success</color>");
                    Debug.LogFormat("<color=green>photonView.Owner {0}</color>", this.name);
                    this.ApplyCustom(customPlayer, customColor);
                }
            }
        }

        public void ApplyCustom(int id, string colorName)
        {
            Debug.LogFormat("<color=yellow>Set id {0}</color>", id);
            foreach (var character in this.characters)
            {
                character.gameObject.SetActive(false);
            }

            this.characters[id].gameObject.SetActive(true);
            var mat = this.characters[id].material;
            Debug.LogFormat("<color=yellow>character : {0}, texture : {1}</color>", id, colorName);
            mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + id + colorName);
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
                if (player.CustomProperties.ContainsKey(this.characterKey))
                {
                    int id = (int)player.CustomProperties[this.characterKey];
                    string colotName = (string)player.CustomProperties[this.colorKey];
                    // 커스텀 적용
                    if (player == PhotonNetwork.LocalPlayer)
                    {
                        ApplyCustom(id, colotName);
                    }
                }
            }
        }
    }

}
