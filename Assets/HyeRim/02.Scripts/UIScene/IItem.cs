using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHR
{
    public interface IItem
    {
        public void Grab(); //������ ����� ��
        public void Use();  //������ ��� ��ȣ�ۿ� �� ��
        public void Release();  //������ ������ ��
    }

}
