using NHR;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Jaewook
{
    /// <summary>
    /// ������ ���� ������ -> OnGrab() ���� ����
    /// </summary>
    public class Knife : ItemObject, IItem
    {
        [Header("��ƼŬ ȿ��")]
        public ParticleSystem particleSys;
        [Header("üũ��")]
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
