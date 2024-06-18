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
        [Header("ĳ���� Ŀ����")]
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


        /// <summary>
        /// �÷��̾� ���� ��Ŀ����������Ƽ�� �÷��̾� ĳ���� Ŀ���� ���� ����ȭ
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="textureName"></param>
        public void SetPlayer(int characterId, string textureName)
        {
            Debug.Log("<color=white>PlayerOn SetCustomProperties</color>");
            //playerCustom["playerCustom"] = (InfoManager.Instance.PlayerInfo.nowCharacterId, InfoManager.Instance.PlayerInfo.nowClothesColorName);
            HashTable props = new HashTable()
            {
                {customKey, (characterId, textureName) }
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }
        public override void OnPlayerPropertiesUpdate(Player _player, HashTable _changedProps) // Ŀ���� ������Ƽ ����� �ݹ� �޴� �Լ�
        {
            //ĳ���� Ŀ����
            if (_changedProps.ContainsKey(customKey) && _player == photonView.Owner)
            {
                var customPlayer = ((int, string))_changedProps[customKey];
                int characterId = customPlayer.Item1;
                string textureName = customPlayer.Item2;
                Debug.LogFormat("<color=white>playerCustom {0}</color>", customPlayer);
                Debug.LogFormat("<color=green>photonView.Owner {0}</color>", this.photonView.Owner.IsLocal);

                if (_player == this.photonView.Owner)
                {
                    Debug.Log("<color=white>playerCustom success</color>");
                    Debug.LogFormat("<color=green>photonView.Owner {0}</color>", this.name);
                    this.ApplyCustom(characterId, textureName);
                }
            }
        }

        public void ApplyCustom(int id, string textureName)
        {
            Debug.LogFormat("<color=yellow>Set id {0}</color>", id);
            foreach (var character in this.characters)
            {
                character.gameObject.SetActive(false);
            }

            this.characters[id].gameObject.SetActive(true);
            var mat = this.characters[id].material;
            Debug.LogFormat("<color=yellow>character : {0}, texture : {1}</color>", id, textureName);
            mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + id + textureName);

        }

        //public void Init()
        //{
        //    //������ ����� ĳ���� �ҷ�����
        //    PhotonView photonview = null;
        //    if (GameManager.Instance.roundManager != null) photonview = GameManager.Instance.roundManager.GetComponent<PhotonView>();
        //    else if(GameManager.Instance.lobbySceneManager != null) photonview = GameManager.Instance.lobbySceneManager.GetComponent<PhotonView>();
        //    var playerCustom = ((int, string))photonview.Owner.CustomProperties["playerCustom"];
        //    var selectedCharacterID = playerCustom.Item1;
        //    var selectedClothesColorName = playerCustom.Item2;

        //    //ĳ���� ����
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
                    var custom = ((int, string))player.CustomProperties[this.customKey];
                    int id = custom.Item1;
                    string textureName = custom.Item2;
                    // Ŀ���� ����
                    if (player == PhotonNetwork.LocalPlayer)
                    {
                        ApplyCustom(id, textureName);
                    }
                }
            }
        }
    }

}
