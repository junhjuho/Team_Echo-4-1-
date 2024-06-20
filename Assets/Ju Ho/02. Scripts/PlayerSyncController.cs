using Photon.Pun;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static UnityEngine.UI.Image;

public class PlayerSyncController : MonoBehaviour
{
    PhotonView pv;
    RiggingManager riggingManager;
    PlayerMovement playerMovement;
    HumanMovement humanMovement;
    MonsterMovement monsterMovement;

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    // xr origin�� ���, �޼�, ������
    public XROrigin origin;
    Transform headRig;
    Transform leftHandRig;
    Transform rightHandRig;

    Transform lefttHandIK_hint;
    Transform rightHandIK_hint;

    RaycastHit hitInfo;

    void Start()
    {
        pv = this.GetComponent<PhotonView>();

        for(int i = 0; i < 4; i++) // �÷��̾� ������ �ڽ��� Jake, Frank, MJ, Zombie�� ����
        { 
            ChangeLayer(this.transform.GetChild(i).gameObject, 7); // �÷��̾� ���̾� ����
        }

        if(pv.IsMine)
        {
            origin = FindObjectOfType<XROrigin>();
            headRig = origin.transform.GetChild(0).GetChild(0);      // xr origin / camera offset / main camera
            leftHandRig = origin.transform.GetChild(0).GetChild(1);  // xr origin / camera offset / left controller
            rightHandRig = origin.transform.GetChild(0).GetChild(2); // xr origin / camera offset / right controller
            riggingManager = this.GetComponentInChildren<RiggingManager>();
        }
    }

    void Update()
    {
        //Debug.Log(headRig.position);

        if (pv.IsMine) // xr origin�� ��ũ ������Ʈ ����ȭ(�������� �Ѱ��ֱ� ����)
        {
            float distance = headRig.transform.position.y - riggingManager.modelHeight; // Ű ����

            origin.transform.position = new Vector3(origin.transform.position.x, origin.transform.position.y - distance, origin.transform.position.z);

            if (headRig.transform.position.y > riggingManager.modelHeight)
            {
                headRig.transform.position = new Vector3(headRig.transform.position.x, riggingManager.modelHeight, headRig.transform.position.z);
                head.transform.position = new Vector3(head.transform.position.x, riggingManager.modelHeight, head.transform.position.z);            
            }

            SyncTransform(head, headRig);
            SyncTransform(leftHand, leftHandRig);
            SyncTransform(rightHand, rightHandRig);
        }
    }

    void SyncTransform(Transform targetTf, Transform rigTf)
    {
        targetTf.position = rigTf.position;
        targetTf.rotation = rigTf.rotation;
    }

    void ChangeLayer(GameObject obj, int layer) // �÷��̾� ���̾� ����
    {
        if(pv.IsMine)
        {
            Renderer[] renderers = obj.transform.GetComponentsInChildren<Renderer>();

            foreach (var renderer in renderers)
            {
                int layerNumber = renderer.gameObject.layer == 14 ? 14 : layer;
                // ���� ���ӿ�����Ʈ�� ���̾ 14(fireaxe)���ٸ� default�� ����
                // �ƴϸ� layer�� ����
                renderer.gameObject.layer = layerNumber;
            }
        }
    }
}
