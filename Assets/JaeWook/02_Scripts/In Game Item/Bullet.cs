//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

///// <summary>
///// 1. prefabs
///// </summary>
//namespace Jaewook
//{
//    public class Bullet : MonoBehaviour
//    {

//        public float damage = 10f;

//        void OnEnable() // setActive - true ���¸� ��� ����
//        {
//            Invoke("ReturnQueue", 2f); // Ư�� ���� ���� ���� -> �Լ� ���� ����
//        }

//        void OnDisable() // false�� ������ ����
//        {
//            if (this.GetComponent<Rigidbody>() != null)
//            {
//                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
//                this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
//            }

//            CancelInvoke("ReturnQueue");
//        }

//        private void ReturnQueue()
//        {
//            ObjectPool.Instance.OnInit(this.gameObject);
//        }

//        /*
//        public void OnTriggerEnter(Collider other)
//        {
//            if (other.transform.root.GetComponent<CharacterHp>() != null)
//            {
//                CharacterHp hp = other.transform.root.GetComponent<CharacterHp>();
//                if (other.CompareTag("Head"))
//                {
//                    // ��弦 ������
//                    hp.OnHit(damage * 3f);
//                }
//                else if (other.CompareTag("Body"))
//                {
//                    // �Ϲ� ������
//                    hp.OnHit(damage);
//                }
//            }
//        }
//        */
//    }
//}
