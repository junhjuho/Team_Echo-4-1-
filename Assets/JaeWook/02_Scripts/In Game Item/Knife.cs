using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaewook
{
    /// <summary>
    /// 복수자 전용 아이템 -> OnGrab() 변신 가능
    /// </summary>
    public class Knife : ItemObject, IItem
    {
        private void Start()
        {
            base.Start();

        }
        public void OnGrab()
        {
            
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
