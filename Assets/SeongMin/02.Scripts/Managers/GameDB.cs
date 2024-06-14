using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeongMin
{
    public class GameDB : MonoBehaviour
    {
        private static GameDB instance;
        public static GameDB Instance { get { return instance; } }
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
        public int playerCount = 0;
        [Header("�ΰ��� ���� �÷��̾��� ������Ʈ")]
        public GameObject myPlayer;
        [Header("�ΰ��� ���� �÷��̾��� �̼�")]
        public PlayerMission playerMission;
        [Header(" �÷��̾� �ΰ��� ��� ������")]
        public bool isWin = false;

        public void Shuffle(int[] _array)
        {
            for(int i = 0; i < _array.Length; i++)
            {
                int temp = _array[i];
                int randNum = Random.Range(0, _array.Length);
                _array[i] = _array[randNum];
                _array[randNum] = temp;
            }
        }

        public void Shuffle(List<GameObject> _list)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                GameObject temp = _list[i];
                int randNum = Random.Range(0, _list.Count);
                _list[i] = _list[randNum];
                _list[randNum] = temp;
            }
        }

        public void Shuffle(List<Transform> _list)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                Transform temp = _list[i];
                int randNum = Random.Range(0, _list.Count);
                _list[i] = _list[randNum];
                _list[randNum] = temp;
            }
        }
        public void Shuffle(List<int> _list)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                int temp = _list[i];
                int randNum = Random.Range(0, _list.Count);
                _list[i] = _list[randNum];
                _list[randNum] = temp;
            }
        }
    }
}