using Photon.Pun;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TreeEditor.TextureAtlas;

namespace NHR
{
    public class PlayerController : MonoBehaviour
    {
        [Header("캐릭터 커스텀")]
        public Character[] characters;

        public SmartWatchCustomInteractable watch;

        private void Awake()
        {
            GameDB.Instance.playerController = this;
            //this.Init();
            if (this.watch == null) this.watch = GetComponentInChildren<SmartWatchCustomInteractable>();
        }
        public void Set(int id)
        {
            Debug.LogFormat("<color=yellow>Set id {0}</color>", id);
            foreach (var character in this.characters)
            {
                character.gameObject.SetActive(false);
            }

            this.characters[id].gameObject.SetActive(true);
        }

        public void Init()
        {
            //인포에 저장된 캐릭터 불러오기
            PhotonView photonview = null;
            if (GameManager.Instance.roundManager != null) photonview = GameManager.Instance.roundManager.GetComponent<PhotonView>();
            else if(GameManager.Instance.lobbySceneManager != null) photonview = GameManager.Instance.lobbySceneManager.GetComponent<PhotonView>();
            var playerCustom = ((int, string))photonview.Owner.CustomProperties["playerCustom"];
            var selectedCharacterID = playerCustom.Item1;
            var selectedClothesColorName = playerCustom.Item2;

            //캐릭터 설정
            //this.characters = this.GetComponentsInChildren<Character>();
            foreach (var character in this.characters)
            {
                character.gameObject.SetActive(false);
            }
            this.characters[selectedCharacterID].gameObject.SetActive(true);
            var mat = this.characters[selectedCharacterID].material;
            Debug.LogFormat("<color=yellow>character : {0}, texture : {1}</color>", selectedCharacterID, selectedClothesColorName);
            mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + selectedCharacterID + selectedClothesColorName);

        }
    }

}
