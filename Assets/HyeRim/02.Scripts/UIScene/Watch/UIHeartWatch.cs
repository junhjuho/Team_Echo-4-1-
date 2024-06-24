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
        public int nowHeart = 3;
        private int maxHeart = 3;

        private void Awake()
        {
            this.hearts = GetComponentsInChildren<UIHeart>();
        }
        private void Start()
        {
            if(GameDB.Instance.playerMission.isChaser) this.gameObject.SetActive(false);

            EventDispatcher.instance.AddEventHandler<int>((int)NHR.EventType.eEventType.Notice_Attacked, new EventHandler<int>((type, heart) =>
            {
                this.nowHeart = heart - 1;
                this.hearts[this.maxHeart - heart].imageDeath.SetActive(true);
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
