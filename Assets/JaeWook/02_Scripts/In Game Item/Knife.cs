using NHR;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Jaewook
{
    /// <summary>
    /// 복수자 전용 아이템 -> OnGrab() 변신 가능
    /// </summary>
    public class Knife : ItemObject, IItem
    {
        // [Header("chaser 아이템 할당")]
        // public List<GameObject> inGameChaserItems = GameManager.Instance.inGameMapManager.inGameChaserItemList;
        [Header("chaser 아이템은 chaser에게만 보이기")]
        public List<GameObject> chaserItemList = GameManager.Instance.inGameMapManager.inGameChaserItemList;
        [Header("chaser인지 아닌지 확인")]
        public CharactorValue charValue = CharactorValue.chaser;
        [Header("배정받은 복수자 미션 아이템 리스트")]
        public GameObject[] chaserMA = GameDB.Instance.playerMission.chaserMissionArray;

        /*
        private void Awake()
        {
            
        }
        */

        private void Start()
        {
            base.Start();

            for (int i = 0; i < chaserMA.Length; i++)
            {
                if (this.gameObject == chaserMA[i])
                {
                    // 이 오브젝트가 chaser에게 배정된 아이템이면 보이기
                    // 그럼 플레이어 정보는 어디서 가져오지..?
                }
            }

        }
        public void OnGrab()
        {
          // 파티클?
        }

        public void OnRelease()
        {
            throw new System.NotImplementedException();
        }

        public void OnUse()
        {
            throw new System.NotImplementedException();
        }
    }

}
