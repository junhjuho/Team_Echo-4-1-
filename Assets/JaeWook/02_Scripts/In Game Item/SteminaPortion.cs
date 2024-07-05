using System.Collections;
using System.Collections.Generic;
using Jaewook;
using UnityEngine;
//using UnityEngine.SocialPlatforms.Impl;

namespace Jaewook
{
    /// <summary>
    /// 스테미너 포션 -> 랜덤 배치 or 주기적으로 리스폰 ( singleton )
    /// </summary>
    public class SteminaPortion : MonoBehaviour, IItem
    {
        public GameObject pickupEffect;
        public GameObject useEffect;

        public void OnGrab()
        {

           
        }

        public void OnUse()
        {

            // stemina 회복 로직
            
        }

        public void OnRelease()
        {
            throw new System.NotImplementedException();
        }
    }

}
