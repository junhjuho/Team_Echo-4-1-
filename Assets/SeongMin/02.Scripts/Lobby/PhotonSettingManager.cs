using NHR;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SeongMin
{
    public class PhotonSettingManager : MonoBehaviourPunCallbacks
    {
        private void Awake()
        {
            GameManager.Instance.photonSettingManager = this;
            Screen.SetResolution(1080, 720, false);
            PhotonNetwork.SendRate = 60;
            PhotonNetwork.SerializationRate = 60;
            PhotonNetwork.GameVersion = "1";
            Connect();

        }

        public void Connect()
        {
            PhotonNetwork.ConnectUsingSettings();
            print("���� ���� ����");
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, null);
            print("���� ���� ���� �Ϸ�");
        }

        public override void OnJoinedRoom()
        {
            print("�濡 �����߽��ϴ�.");
            var _player = PhotonNetwork.Instantiate("Player", Vector3.up, Quaternion.identity);
            GameManager.Instance.lobbySceneManager.playerMission = _player.GetComponent<PlayerMission>();
            GameManager.Instance.lobbySceneManager.playerController = _player.GetComponent<PlayerController>();
            //GameManager.Instance.lobbySceneManager.playerController.photonView.RPC("ApplyCustom", RpcTarget.AllBuffered);

            //Debug.LogFormat("<color=green>{0}</color>", photonView);
            //foreach (Player player in PhotonNetwork.PlayerList)
            //{

            //    if (player.CustomProperties.ContainsKey("playerCustom"))
            //    {
            //        Debug.Log("<color=yellow>playerSet</color>");

            //        int id = (int)player.CustomProperties["playerCustom"];
            //        // �� �÷��̾��� ������ �����մϴ�.
            //        if (player == PhotonNetwork.LocalPlayer)
            //        {
            //            //UIManager.Instance.robbySceneMenu.SetPlayer(id);
            //            GameManager.Instance.lobbySceneManager.playerController.ApplyCustom(id);
            //        }
            //    }
            //}

            //if (GameManager.Instance.lobbySceneManager.playerMission.photonView.IsMine)
            //{
            //    Debug.LogFormat("<color=green>{0}</color>", UIManager.Instance);
            //    Debug.LogFormat("<color=green>{0}</color>", UIManager.Instance.robbySceneMenu);
            //    int id = InfoManager.Instance.PlayerInfo.nowCharacterId;
            //    UIManager.Instance.robbySceneMenu.SetPlayer(id);
            //}


            //ĳ���� Ŀ���� ����
            //GameManager.Instance.lobbySceneManager.photonView.RPC("InitPlayerSetting", RpcTarget.All);

            if (PhotonNetwork.IsMasterClient)
            GameManager.Instance.lobbySceneManager.isLobbySetting = true; // �κ� ���� �������� �˸���
        }


    }

}
