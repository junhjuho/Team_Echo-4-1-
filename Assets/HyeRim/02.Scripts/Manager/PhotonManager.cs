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
        public void OnPlayer() // 플레이어가 버튼을 눌러 커스텀 프로퍼티 변경시 준비완료한 플레이어 숫자 동기화 
        {
            HashTable onPlayer = new HashTable
                {
                    {"onPlayer", true}
                };

            PhotonNetwork.LocalPlayer.SetCustomProperties(onPlayer); // 커스텀 프로퍼티 변경하는 함수
        }
        public override void OnPlayerPropertiesUpdate(Player _player, HashTable _changedProps) // 커스텀 프로퍼티 변경시 콜백 받는 함수
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