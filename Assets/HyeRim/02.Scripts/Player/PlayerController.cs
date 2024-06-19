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
            this.Init();
            if (this.watch == null) this.watch = GetComponentInChildren<SmartWatchCustomInteractable>();
        }

        private void Init()
        {
            //인포에 저장된 캐릭터 불러오기
            var selectedCharacterID = InfoManager.Instance.PlayerInfo.nowCharacterId;
            var selectedClothesColorName = InfoManager.Instance.PlayerInfo.nowClothesColorName;

            //캐릭터 설정
            this.characters = this.GetComponentsInChildren<Character>();
            foreach (var character in this.characters)
            {
                character.gameObject.SetActive(false);
            }
            this.characters[selectedCharacterID].gameObject.SetActive(true);
            var mat = this.characters[selectedCharacterID].material;
            Debug.LogFormat("<color=yellow>material : {0}</color>", mat.name);
            mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + selectedCharacterID + selectedClothesColorName);

        }
    }

}
