using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeongMin 
{
    /// <summary>
    /// �� ��ũ��Ʈ�� �׽�Ʈ �� ���� ��� ����
    /// </summary>
    public class PlayerData : MonoBehaviour
    {
        public GameObject[] characterArray = new GameObject[10];
        private void Awake()
        {
            GameDB.Instance.myPlayer = this.gameObject;
            
        }
    }
}
