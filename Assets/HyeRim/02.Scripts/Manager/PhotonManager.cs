using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using HashTable = ExitGames.Client.Photon.Hashtable;

namespace NHR
{
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        private void Awake()
        {
            SeongMin.GameManager.Instance.photonManager = this;
        }
        public void OnPlayer() // �÷��̾ ��ư�� ���� Ŀ���� ������Ƽ ����� �غ�Ϸ��� �÷��̾� ���� ����ȭ 
        {
            HashTable onPlayer = new HashTable
                {
                    {"onPlayer", true}
                };

            PhotonNetwork.LocalPlayer.SetCustomProperties(onPlayer); // Ŀ���� ������Ƽ �����ϴ� �Լ�
        }
        public override void OnPlayerPropertiesUpdate(Player _player, HashTable _changedProps) // Ŀ���� ������Ƽ ����� �ݹ� �޴� �Լ�
        {
            if (_changedProps.ContainsKey("onPlayer"))
            {
                if (_player == PhotonNetwork.LocalPlayer)
                {
                    //GameDB.Instance.playerController.Init();
                    SeongMin.GameManager.Instance.playerManager.playerController.Init();
                }
            }
        }

    }

}