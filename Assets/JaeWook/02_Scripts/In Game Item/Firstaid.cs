using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeongMin;

namespace Jaewook
{
    public class Firstaid : ItemObject, IItem
    {
        public GameObject useEffect;

        public void OnGrab()
        {
            // 
        }

        public void OnUse()
        {
            // HP 회복 효과
            Instantiate(useEffect, this.transform.position, Quaternion.identity);

            // HP 회복 (시스템, UI 전달)



        }

        public void OnRelease()
        {
            throw new System.NotImplementedException();
        }

    }

}
