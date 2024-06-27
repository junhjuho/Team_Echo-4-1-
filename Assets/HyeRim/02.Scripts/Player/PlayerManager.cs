using Photon.Pun;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace NHR
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("플레이어 제어")]
        public PlayerController playerController;
        [Header("플레이어 UI")]
        public UIPlayer uiPlayer;

        [Header("남은 생명 수")]
        public int heart = 2;

        [Header("인간 플레이어 움직임")]
        public HumanMovement humanMovement;

        private void Awake()
        {
            SeongMin.GameManager.Instance.playerManager = this;
            this.heart = 2;
        }
        private void Start()
        {
            ////공격받음
            //EventDispatcher.instance.AddEventHandler<int>((int)NHR.EventType.eEventType.Notice_Attacked, new EventHandler<int>((type, heart) =>
            //{
            //    //this.heart = heart - 1;
            //    if (this.heart <= 0)
            //        GameManager.Instance.roundManager.photonView.RPC("AllPlayerLobbySceneLoad", RpcTarget.MasterClient);
            //}));

        }
    }
}
