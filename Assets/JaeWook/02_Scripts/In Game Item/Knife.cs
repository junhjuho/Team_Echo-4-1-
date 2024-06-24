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
        [Header("파티클 효과")]
        public ParticleSystem particleSys;
        [Header("체크용")]
        public PlayerMission playerMission;

        private void Start()
        {
            base.Start();

            playerMission = GameDB.Instance.playerMission;

        }


        public virtual void OnGrab()
        {

        }

      

        public void OnRelease()
        {
            //throw new System.NotImplementedException();
        }

        public void OnUse()
        {
            //throw new System.NotImplementedException();
        }


    }

}
