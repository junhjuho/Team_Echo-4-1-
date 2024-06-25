using Jaewook;
using NHR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeongMin
{
    public class GameDB : MonoBehaviour
    {
        private static GameDB instance;
        public static GameDB Instance { get { return instance; } }
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
        public int playerCount = 0;
        [Header("인게임 로컬 XROrigin의 오브젝트")]
        public GameObject myPlayer;
        [Header("플레이어 손전등")]
        public FlashLight myFlashLight;
        [Header("인게임 로컬 플레이어의 미션")]
        public PlayerMission playerMission;
        [Header("인게임 로컬 플레이어의 컨트롤러")]
        public PlayerController playerController;
        [Header(" 플레이어 인게임 결과 데이터")]
        public bool hasGameData = false;
        public bool isWin = false;
        [Header("XROrigin 버그 픽스용 오브젝트")]
        public XROriginBugFixed xrOriginBugFixedObject;
        [Header("인게임 아이템 정보UI")]
        public ItemInfomationCanvas itemInfomationCanvas;
        [Header("탈출 지점 리스트")]
        public List<Transform> escapeDoorPositionList;

        public void Shuffle(int[] _array)
        {
            for(int i = 0; i < _array.Length; i++)
            {
                int temp = _array[i];
                int randNum = Random.Range(0, _array.Length);
                _array[i] = _array[randNum];
                _array[randNum] = temp;
            }
        }

        public void Shuffle(List<GameObject> _list)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                GameObject temp = _list[i];
                int randNum = Random.Range(0, _list.Count);
                _list[i] = _list[randNum];
                _list[randNum] = temp;
            }
        }

        public void Shuffle(List<Transform> _list)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                Transform temp = _list[i];
                int randNum = Random.Range(0, _list.Count);
                _list[i] = _list[randNum];
                _list[randNum] = temp;
            }
        }
        public void Shuffle(List<int> _list)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                int temp = _list[i];
                int randNum = Random.Range(0, _list.Count);
                _list[i] = _list[randNum];
                _list[randNum] = temp;
            }
        }
    }
}