using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NHR
{
    public interface ICharacterObserver
    {
        //������ ������Ʈ ĳ���͹�ȣ, �ؽ��� �̸�
        void ObserverUpdate(int characterNum, string textureName);
    }

    public class CustomPlayer : MonoBehaviour, ICharacterObserver
    {
        //���� ĳ���͹�ȣ, �ؽ��� �̸�
        public int nowCharacterNum;
        public string nowTextureName;

        public TMP_Text characterName;

        public Character[] characters;

        private void Awake()
        {
            this.Init();
        }
        private void Init()
        {
            this.characters = this.GetComponentsInChildren<Character>();
            //foreach (var character in this.characters)
            //{
            //    character.gameObject.SetActive(false);
            //}
            //this.characters[this.nowCharacterNum].gameObject.SetActive(true);
            this.characterName.text = this.characters[this.nowCharacterNum].name;
        }
        public void ObserverUpdate(int characterNum, string textureName)
        {
            Debug.LogFormat("in CustomPlayer num : {0}, texture : {1}", characterNum, textureName);
            //���޵� ���� ������Ʈ
            //���� ���ð� �ٸ� ���
            //if (this.nowCharacterNum != characterNum)
            //{
            //    //ĳ���� ����
            //    this.characters[this.nowCharacterNum].gameObject.SetActive(false);
            //    this.nowCharacterNum = characterNum;
            //    this.characters[this.nowCharacterNum].gameObject.SetActive(true);
            //    this.characterName.text = this.characters[this.nowCharacterNum].name;
            //}
            //�ؽ��� �ٲ�
            var mat = this.characters[this.nowCharacterNum].material;
            Debug.LogFormat("<color=yellow>material : {0}</color>", mat.name);
            //Debug.LogFormat("<color=yellow>main texture : {0}</color>", mat.mainTexture.name);
            mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + this.nowCharacterNum + textureName);

            this.nowTextureName = textureName;
        }
    }
}
