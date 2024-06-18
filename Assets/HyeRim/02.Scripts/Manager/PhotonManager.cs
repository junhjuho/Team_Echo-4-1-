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
        HashTable playerCustom = new HashTable();

        private void Awake()
        {
            SeongMin.GameManager.Instance.photonManager = this;
        }
        /// <summary>
        /// �÷��̾� ���� ��Ŀ����������Ƽ�� �÷��̾� ĳ���� Ŀ���� ���� ����ȭ
        /// </summary>
        public void SetPlayer()
        {
            Debug.Log("<color=white>PlayerOn SetCustomProperties</color>");
            //playerCustom["playerCustom"] = (InfoManager.Instance.PlayerInfo.nowCharacterId, InfoManager.Instance.PlayerInfo.nowClothesColorName);
            playerCustom["playerCustom"] = (0, "Green");
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustom);
        }

        public override void OnPlayerPropertiesUpdate(Player _player, HashTable _changedProps) // Ŀ���� ������Ƽ ����� �ݹ� �޴� �Լ�
        {
            if (_changedProps.ContainsKey("playerCustom"))
            {
                var customPlayer = ((int, string))_changedProps["playerCustom"];

                if (_player == PhotonNetwork.LocalPlayer)
                {
                    //GameDB.Instance.playerController.Init();
                    GameManager.Instance.lobbySceneManager.playerController.Init();
                }
            }
        }

    }

}