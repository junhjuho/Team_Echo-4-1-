using System.Collections;
using System.Collections.Generic;
using Jaewook;
using SeongMin;
using UnityEngine;

namespace Jaewook
{
    /// <summary>
    /// �̼� ������, OnGrab() -> �̼� �ϼ�
    /// </summary>
    public class KeyItem : ItemObject, IItem
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
