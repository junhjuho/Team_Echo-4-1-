using Oculus.Interaction.UnityCanvas;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static NHR.App;
using static NHR.Enums;

namespace NHR
{
    public interface ICharacterUISubject
    {
        //������ ����ϱ�
        void ResisterObserver(ICharacterObserver observer);
        //������ �����ϱ�
        void RemoveObserver(ICharacterObserver observer);
        //��� ������ ������Ʈ�ϱ�
        void UpdateObservers();
    }

    public class UICharacterCustomManager : MonoBehaviour, ICharacterUISubject
    {
        //���õ� ĳ����, �÷�
        public UICharacterSlot selectedCharacter;
        public UIClothesColor selectedClothesColor;

        //������ ������
        private List<ICharacterObserver> observers = new List<ICharacterObserver>();
        public ICharacterObserver observer;

        //ĳ���� ���Ե�
        //public UICharacterSlot[] slots;
        //�ӽ� only Jake
        public UICharacterSlot slot;

        //�� �÷� ���Ե�
        public UIClothesColor[] colors;

        //Ÿ��Ʋ�� ���� ��ư
        public Button buttonExit;

        //curvedUI
        public CanvasRenderTexture canvasRender;
        public CanvasMeshRenderer canvasMeshRenderer;

        private void Awake()
        {
            //����
            DataManager.Instance.LoadCharacterData();
            InfoManager.Instance.LoadPlayerInfo();

            this.Init();
        }
        private void Init()
        {
            //ĳ���� ���Ե� �޾ƿ���
            //this.slots = this.GetComponentsInChildren<UICharacterSlot>();

            //�ӽ� only Jake
            this.OnlyJake();

            //ĳ���� ���Ե�
            //for (int i = 0; i < this.slots.Length; i++)
            //{
            //    this.slots[i].Init();
            //    //ĳ���� ��ȣ
            //    this.slots[i].characterNum = i;
            //    //ĳ���� �̸� �޾ƿ���
            //    Debug.Log($"id : {1000 + i}");

            //    var data = DataManager.Instance.GetCharacterData(1000 + i);
            //    this.slots[i].textCharacterName.text = data.name;
            //    this.slots[i].imageCharacter.sprite = Resources.Load<Sprite>(data.texturePath);
            //}
            //�� �÷���
            for (int i = 0; i < this.colors.Length; i++) this.colors[i].index = i;

            //������ ����� ĳ���� �ҷ�����
            this.selectedCharacter = this.slot;
            this.selectedClothesColor = this.colors[InfoManager.Instance.PlayerInfo.nowCharacterId];
        }

        //�ӽ�
        private void OnlyJake()
        {
            this.slot = this.GetComponentInChildren<UICharacterSlot>();
            this.slot.Init();
            this.slot.characterNum = 1;

            var data = DataManager.Instance.GetCharacterData(1001);
            this.slot.textCharacterName.text = data.name;
            this.slot.imageCharacter.sprite = Resources.Load<Sprite>(data.texturePath);
        }
        private void Start()
        {
            Debug.Log("UICharacterCustomManager start");

            //����Ʈ�� ���õ� ĳ����/�� Ȱ��ȭ
            this.selectedCharacter.OnSelected();
            this.selectedClothesColor.selectedGo.SetActive(true);

            //��ư�� ����, Ŭ�� �� OnBtnClick �Լ� ȣ��
            //foreach (UICharacterSlot slot in this.slots)
            //{
            //    slot.buttonCharacterSlot.onClick.AddListener(() => OnBtnClick(slot));
            //}
            //�ӽ�
            this.slot.buttonCharacterSlot.onClick.AddListener(() => this.OnBtnClick(slot));

            foreach(UIClothesColor color in this.colors)
            {
                color.btnColor.onClick.AddListener(() => OnBtnColorClick(color));
            }

        }

        private void OnBtnClick(UICharacterSlot slot)
        {
            //��ư Ŭ�� �� �������� ������Ʈ, ĳ����
            Debug.LogFormat("{0} clicked", slot);
            //���� ���õ� ĳ���� �÷� ȸ������, ���� ������ ĳ���� �÷� �������
            //���� ������ �Ͱ� ���� �ʴٸ�
            //if (this.selectedCharacter != slot)
            //{
            //    this.selectedCharacter.OnUnselected();
            //    this.selectedCharacter = slot;
            //    this.selectedCharacter.OnSelected();
            //    this.UpdateObservers();
            //}
        }
        private void OnBtnColorClick(UIClothesColor color)
        {
            //��ư Ŭ�� �� �������� ������Ʈ, �� �÷�
            Debug.LogFormat("{0} clicked", color);
            //���� ������ �Ͱ� ���� �ʴٸ�
            if (this.selectedClothesColor != color)
            {
                this.selectedClothesColor.selectedGo.SetActive(false);
                this.selectedClothesColor = color;
                this.selectedClothesColor.selectedGo.SetActive(true);
                this.UpdateObservers();
            }
        }
        public void UpdateObservers()
        {
            Debug.Log("Update Observers");
            Debug.LogFormat("num : {0}, color name : {1}", this.selectedCharacter, this.selectedClothesColor);
            //��� ������ ������Ʈ�ϱ�
            foreach (ICharacterObserver observer in observers)
            {
                observer.ObserverUpdate(this.selectedCharacter.characterNum, this.selectedClothesColor.textureName);
            }
            //curvedUI ������Ʈ
            //EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Update_CurvedUI);
            //canvasRender.UpdateCamera();
            //canvasMeshRenderer.UpdateUI();
            //this.canvasRender.UpdateCurved();
        }

        public void RemoveObserver(ICharacterObserver observer)
        {
            //������ �����ϱ�
            this.observers.Remove(observer);
        }

        public void ResisterObserver(ICharacterObserver observer)
        {
            //������ ����ϱ�
            this.observers.Add(observer);
        }
    }

}

