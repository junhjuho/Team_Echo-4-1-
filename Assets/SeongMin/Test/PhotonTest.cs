using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeongMin
{
    public class PhotonTest : MonoBehaviourPunCallbacks
    {
        private void Awake()
        {
            Screen.SetResolution(1080, 720, false);
            PhotonNetwork.SendRate = 60;
            PhotonNetwork.SerializationRate = 30;
            PhotonNetwork.GameVersion = "1";



        }
        private void Start()
        {
            OnConnect();
        }
        public void OnConnect()
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        public override void OnConnectedToMaster() // �����Ϳ��� ����ɶ� ȣ���Լ� ������ ���� ������
        {
            PhotonNetwork.JoinOrCreateRoom("Room",new RoomOptions {MaxPlayers = 20 },null);
            Debug.Log("�� ����");
        }
        public override void OnJoinedRoom() // �濡 �����Ҷ�
        {
            print("������");
        }
    }

}
