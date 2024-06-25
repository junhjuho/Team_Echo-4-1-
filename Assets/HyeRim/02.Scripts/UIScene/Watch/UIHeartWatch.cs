using NHR;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHR
{
    public class UIHeartWatch : MonoBehaviour
    {
        public UIHeart[] hearts;
        public int attackCount = 0;
        private int maxHeart = 3;

        private void Awake()
        {
            this.hearts = GetComponentsInChildren<UIHeart>();
            this.attackCount = 0;
        }
        private void Start()
        {
            if(GameDB.Instance.playerMission.isChaser) this.gameObject.SetActive(false);

            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Notice_Attacked, new EventHandler((type) =>
            {
                Debug.Log("미션 옆 하트 업데이트");
                this.hearts[this.attackCount].imageDeath.SetActive(true);
                this.attackCount++;
            }));

        }
        //public void UpdateHeart()
        //{
        //    Debug.Log("Heart Enabled");
        //    if (!GameDB.Instance.playerMission.isChaser)
        //    {
        //        this.nowHeart = SeongMin.GameManager.Instance.playerManager.heart;
        //        if (this.nowHeart < this.maxHeart)
        //        {
        //            for (int i = 0; i < 3 - this.nowHeart; i++)
        //            {
        //                this.hearts[i].imageDeath.SetActive(true);
        //            }
        //        }

        //    }
        //}
    }
}
