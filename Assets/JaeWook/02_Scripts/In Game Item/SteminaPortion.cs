using System.Collections;
using System.Collections.Generic;
using Jaewook;
using UnityEngine;
//using UnityEngine.SocialPlatforms.Impl;

namespace Jaewook
{
    /// <summary>
    /// ���׹̳� ���� -> ���� ��ġ or �ֱ������� ������ ( singleton )
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

            // stemina ȸ�� ����
            
        }

        public void OnRelease()
        {
            throw new System.NotImplementedException();
        }
    }

}
