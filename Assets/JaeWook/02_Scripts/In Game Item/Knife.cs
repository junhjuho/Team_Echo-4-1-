using NHR;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Jaewook
{
    /// <summary>
    /// ������ ���� ������ -> OnGrab() ���� ����
    /// </summary>
    public class Knife : ItemObject, IItem
    {
        // [Header("chaser ������ �Ҵ�")]
        // public List<GameObject> inGameChaserItems = GameManager.Instance.inGameMapManager.inGameChaserItemList;
        [Header("chaser �������� chaser���Ը� ���̱�")]
        public List<GameObject> chaserItemList = GameManager.Instance.inGameMapManager.inGameChaserItemList;
        [Header("chaser���� �ƴ��� Ȯ��")]
        public CharactorValue charValue = CharactorValue.chaser;
        [Header("�������� ������ �̼� ������ ����Ʈ")]
        public GameObject[] chaserMA = GameDB.Instance.playerMission.chaserMissionArray;

        /*
        private void Awake()
        {
            
        }
        */

        private void Start()
        {
            base.Start();

            for (int i = 0; i < chaserMA.Length; i++)
            {
                if (this.gameObject == chaserMA[i])
                {
                    // �� ������Ʈ�� chaser���� ������ �������̸� ���̱�
                    // �׷� �÷��̾� ������ ��� ��������..?
                }
            }

        }
        public void OnGrab()
        {
          // ��ƼŬ?
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
