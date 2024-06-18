using NHR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeongMin
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance { get { return instance; } }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
        [Header("Ʃ�丮�� �� �Ŵ���")]
        public TutorialSceneManager tutorialSceneManager;
        [Header("�κ� �� �Ŵ���")]
        public LobbySceneManager lobbySceneManager;
        public PhotonSettingManager photonSettingManager;
        [Header("�� ���� �Ŵ���")]
        public InGameSceneManager inGameSceneManager;
        public InGameMapManager inGameMapManager;
        public RoundManager roundManager;
        public MissionManager missionManager;
        public RoundTimer roundTimer;
        public PlayerManager playerManager;
        public PhotonManager photonManager;
    }

}