using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaewook
{
    /// <summary>
    /// ��� Item, Interface ��� ����
    /// </summary>
    public interface IItem
    {
        // Item ��ȣ�ۿ� ���� 
        public void OnGrab();

        // item Ʈ���Ź�ư �۵�
        public void OnUse();

        // item ���� ��
        public void OnRelease();

        // �߰� ��ȣ�ۿ�
        // public void On??();

    }
}
