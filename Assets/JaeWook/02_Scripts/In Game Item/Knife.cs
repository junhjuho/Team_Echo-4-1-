using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaewook
{
    public class Knife : ItemObject, IItem
    {
        private void Start()
        {
            base.Start();

        }
        public void OnGrab()
        {
            // 복수자 전용 아이템 -> 집으면 변신 가능
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
