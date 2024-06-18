using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TreeEditor.TextureAtlas;

namespace NHR
{
    public class PlayerController : MonoBehaviour
    {
        [Header("ĳ���� Ŀ����")]
        public Character[] characters;

        public SmartWatchCustomInteractable watch;

        private void Awake()
        {
            GameDB.Instance.playerController = this;
            foreach (var character in this.characters)
            {
                character.gameObject.SetActive(false);
            }
            //this.Init();
            if (this.watch == null) this.watch = GetComponentInChildren<SmartWatchCustomInteractable>();
        }

        public void Init()
        {
            //������ ����� ĳ���� �ҷ�����
            var selectedCharacterID = InfoManager.Instance.PlayerInfo.nowCharacterId;
            var selectedClothesColorName = InfoManager.Instance.PlayerInfo.nowClothesColorName;

            //ĳ���� ����
            //this.characters = this.GetComponentsInChildren<Character>();
            this.characters[selectedCharacterID].gameObject.SetActive(true);
            var mat = this.characters[selectedCharacterID].material;
            Debug.LogFormat("<color=yellow>character : {0}, texture : {1}</color>", selectedCharacterID, selectedClothesColorName);
            mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + selectedCharacterID + selectedClothesColorName);

        }
    }

}
