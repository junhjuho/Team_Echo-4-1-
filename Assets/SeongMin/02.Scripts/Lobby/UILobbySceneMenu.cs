using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static NHR.App;
using HashTable = ExitGames.Client.Photon.Hashtable;

namespace SeongMin
{
    public class UILobbySceneMenu : MonoBehaviourPunCallbacks
    {
        [Header("��ư��")]
        public Button readyButton;
        public Button quitButton;
        public Button roundStartButton;
        [Header("�ִ� �÷��̾� ���� �ϱ�")]
        public int maxPlayer = 4;
        [Header("�غ�� �÷��̾� ǥ�õǴ� ��")]
        public int readyPlayer = 0;
        public bool isReady = false;

        private void Awake()
        {
            UIManager.Instance.robbySceneMenu = this;
            readyButton = transform.Find("ReadyButton").GetComponent<Button>();
            quitButton = transform.Find("QuitButton").GetComponent<Button>();
            roundStartButton = transform.Find("RoundStartButton").GetComponent<Button>();

            readyButton.onClick.AddListener(() => PlayerReady());
            quitButton.onClick.AddListener(() =>
            {
                PhotonNetwork.Disconnect();  //��Ʈ��ũ ���� ����
                EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Title);
            });
            roundStartButton.onClick.AddListener(() => GameStart());

            roundStartButton.gameObject.SetActive(false);

            PhotonNetwork.AutomaticallySyncScene = true; // ���� ���� �ִ� �÷��̾�� �ڵ� ����ȭ
        }

        private void PlayerReady() // �÷��̾ ��ư�� ���� Ŀ���� ������Ƽ ����� �غ�Ϸ��� �÷��̾� ���� ����ȭ 
        {
            isReady = !isReady;
            HashTable props = new HashTable
            {
                {"isReady", isReady}
            };

            PhotonNetwork.LocalPlayer.SetCustomProperties(props); // Ŀ���� ������Ƽ �����ϴ� �Լ�
        }

        public override void OnPlayerPropertiesUpdate(Player _player, HashTable _changedProps) // Ŀ���� ������Ƽ ����� �ݹ� �޴� �Լ�
        {
            if (_changedProps.ContainsKey("isReady"))
            {
                bool reddystate = (bool)_changedProps["isReady"];

                if (_player == PhotonNetwork.LocalPlayer)
                {
                    readyButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = reddystate ? "Not Ready" : "Ready"; // Ready ������ Not Ready�� �ٲ�
                }
                // Update the number of ready players
                UpdateReadyPlayerCount();
                PlayersReadyCheck();
            }
        }

        private void UpdateReadyPlayerCount() // �ܼ� �����ο� üũ�� �Լ� (������ �������)
        {
            readyPlayer = 0;
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (player.CustomProperties.TryGetValue("isReady", out object isReady))
                {
                    if ((bool)isReady)
                        readyPlayer++;
                    else
                        readyPlayer--;
                }
            }
        }

        public void PlayersReadyCheck()
        {
            bool allReady = true;
            // ���� ������ �÷��̾���� �Ѹ��̶� ���� ���°� �ƴϸ� allReady�� false�� ����
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (!player.CustomProperties.TryGetValue("isReady", out object isReddy) || !(bool)isReddy)
                {
                    allReady = false;
                    break;
                }
            }

            if (allReady && PhotonNetwork.IsMasterClient)
            {
                roundStartButton.gameObject.SetActive(true);
                print("��� �÷��̾ �غ� �Ϸ��Դϴ�.");
            }
            else
            {
                roundStartButton.gameObject.SetActive(false);
                print("���� �������� ���� �÷��̾ �ֽ��ϴ�");
            }
        }

        private void GameStart()
        {
            if (PhotonNetwork.IsMasterClient)
            {

                GameDB.Instance.playerCount = PhotonNetwork.PlayerList.Length;
                PhotonNetwork.LoadLevel("InGameScene 1"); // ��� �÷��̾�� �ΰ��� ������ �̵�
            }
        }
    }
}