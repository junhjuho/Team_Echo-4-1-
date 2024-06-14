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
            // ���׹̳� ������ ȿ��
            Instantiate(pickupEffect, this.transform.position, Quaternion.identity);

           
        }

        public void OnUse()
        {
            // ���׹̳� ȸ�� ȿ��
            Instantiate(useEffect, this.transform.position, Quaternion.identity);
            Debug.Log("Health Potion Used");

            // stemina ȸ�� ����
            
        }

        public void OnRelease()
        {
            throw new System.NotImplementedException();
        }
    }

}
