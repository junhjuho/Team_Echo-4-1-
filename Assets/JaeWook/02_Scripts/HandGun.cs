//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class HandGun : MonoBehaviour, Jaewook.IItem
//{
//    public Transform fireTF;
//    public float shootSpeed = 10f;

//    public void OnGrab()
//    {
//        //
//    }

//    public void OnUse()
//    {
//        Debug.Log("�ڵ���� ����ߴ�");
//        GameObject bullet = ObjectPool.Instance.OnDequeue();

//        // Gun�� fire transfrom ��ġ�� �ʱ�ȭ
//        bullet.transform.SetPositionAndRotation(fireTF.position, fireTF.rotation);

//        // �Ѿ� ����, �߻�
//        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
//        bulletRb.AddForce(fireTF.forward * shootSpeed, ForceMode.Impulse);
        
//    }
//}
