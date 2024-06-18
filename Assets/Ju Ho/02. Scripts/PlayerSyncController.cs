using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
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

    // xr origin의 헤드, 왼손, 오른손
    Transform headRig;
    Transform leftHandRig;
    Transform rightHandRig;
    XROrigin origin;

    Transform lefttHandIK_hint;
    Transform rightHandIK_hint;


    RaycastHit hitInfo;

    void Start()
    {
        pv = this.GetComponent<PhotonView>();

        ChangeLayer(this.gameObject, 7); // 플레이어 레이어 설정
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
        if (pv.IsMine)
        {
            headRig.transform.position = origin.transform.position;

            if (headRig.transform.position.y > riggingManager.modelHeight)
            {
                float floor = headRig.transform.position.y > 5f ? 6.1f : 0f;

                headRig.transform.position = new Vector3(headRig.transform.position.x, (riggingManager.modelHeight) + floor, headRig.transform.position.z);
                head.transform.position = new Vector3(head.transform.position.x, (riggingManager.modelHeight) + floor, head.transform.position.z);
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

    void ChangeLayer(GameObject obj, int layer) // 플레이어 레이어 설정
    {
        if(pv.IsMine)
        {
            obj.layer = this.transform.GetChild(3).gameObject.activeSelf ? 13 : layer;
            // 좀비 오브젝트가 켜져있으면 레이어는 13번(좀비), 아니면 7번(플레이어)
            foreach (Transform child in obj.transform)
            {
                ChangeLayer(child.gameObject, layer); 
            }
        }
    }
}
