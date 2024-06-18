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
        /// 플레이어 접속 시커스텀프로퍼티에 플레이어 캐릭터 커스텀 정보 동기화
        /// </summary>
        public void SetPlayer()
        {
            Debug.Log("<color=white>PlayerOn SetCustomProperties</color>");
            //playerCustom["playerCustom"] = (InfoManager.Instance.PlayerInfo.nowCharacterId, InfoManager.Instance.PlayerInfo.nowClothesColorName);
            playerCustom["playerCustom"] = (0, "Green");
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustom);
        }

        public override void OnPlayerPropertiesUpdate(Player _player, HashTable _changedProps) // 커스텀 프로퍼티 변경시 콜백 받는 함수
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