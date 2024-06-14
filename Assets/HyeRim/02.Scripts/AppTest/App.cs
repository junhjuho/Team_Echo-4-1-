using Photon.Pun;
using Photon.Realtime;
using SeongMin;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


namespace NHR
{
    public class App : MonoBehaviour
    {
        //�� enum
        public enum eSceneType
        {
            App, Title, Lobby, InGame, Loading, Tutorial, CharacterCustom
        }
        //���� ��
        private eSceneType nowScene = eSceneType.Title;
        //���� ��
        private eSceneType preScene = eSceneType.Title;


        //�����ΰ�?
        public bool isNewbie;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            //�ʱ���õ�
            this.Init();
        }

        //�ʱ�ȭ
        private void Init()
        {
        }
        private void Start()
        {
            //������ �޾ƿ���
            DataManager.Instance.LoadCharacterData();
            DataManager.Instance.LoadTutorialData();
            DataManager.Instance.LoadEventDialogData();

            //�÷��̾� ������ ���� �Ǻ��ϱ�
            this.isNewbie = InfoManager.Instance.IsNewbie();

            //���� �����̸� ���� �޾ƿ���
            //�ű� �����̸� ���� ����
            if (isNewbie)
            {
                //�ű� ����
                Debug.Log("<color=yellow>�ű�����</color>");

                //���� �ʱ�ȭ
                InfoManager.Instance.PlayerInfoInit();
                InfoManager.Instance.HeightInfoInit();
            }
            else
            {
                //���� ����
                Debug.Log("<color=yellow>��������</color>");
            
                //���� �ε�
                InfoManager.Instance.LoadPlayerInfo();
                //�ӽ� �� �÷��� �ʱ�ȭ
                InfoManager.Instance.HeightInfoInit();
            }


            //EventDispatcher
            //�Ű������� ���� �̺�Ʈ �ʱ�ȭ
            //EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Change_Scene, new EventHandler((type) =>
            //{
            //    Debug.Log("<color=yellow>Event_Test</color>");
            //}));


            //eSceneType �Ű������� �ִ� �̺�Ʈ �ʱ�ȭ
            EventDispatcher.instance.AddEventHandler<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, new EventHandler<eSceneType>((type, scene) =>
            {
                Debug.Log("change Scene");
                this.preScene = this.nowScene;
                //this.nowScene = scene;
                this.ChangeScene(scene);
            }));

            //���� ���� �� ������ PlayerPref ���� �ҷ�����

            //���� �� �ε�
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Title);
        }


        //�� ��ȯ
        public void ChangeScene(eSceneType sceneType)
        {
            //�ε���
            //���� sceneType�� ���� �� ��ȯ switch
            //�޸𸮸� �޾ƿ��� �� �ε��� �ε�
            //var oper = SceneManager.LoadSceneAsync("Loading");
            //oper.completed += (obj) =>
            //{
            //    var loadinMain = GameDB.FindFirstObjectByType<LoadingMain>();
            //};

            //��Ÿ���� Main�̸�
            if(sceneType == eSceneType.Title)
            {
                //���ε� (�޸�)
                var mainOper = SceneManager.LoadSceneAsync(sceneType.ToString() + "Scene 1");
                mainOper.completed += (obj) =>
                {
                    //���ε� �Ϸ�
                    //�ν��Ͻ� ���� ����
                    //var main = GameObject.FindObjectOfType<Title>();
                    //main.Init();
                };
                SceneManager.LoadScene(sceneType.ToString() + "Scene 1");
                this.nowScene = sceneType;
            }
            else
            {
                SceneManager.LoadScene(sceneType.ToString() + "Scene 1");
                Debug.LogFormat("<color=yellow>nowScene : {0}, preScene : {1}</color>", this.nowScene, this.preScene);
                if (sceneType == eSceneType.Lobby )//&&�÷��̾�� ���� ��� ������
                {
                    Debug.Log("���� ���");
                    //EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Notice_GameResult);
                }

                this.nowScene = sceneType;
            }

        }

        //�ʿ��� �͵�..
    }

}