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
            // HP ȸ�� ȿ��
            Instantiate(useEffect, this.transform.position, Quaternion.identity);

            // HP ȸ�� (�ý���, UI ����)



        }

        public void OnRelease()
        {
            throw new System.NotImplementedException();
        }

    }

}
