using Photon.Pun;
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

        for(int i = 0; i < 4; i++) // 플레이어 프리팹 자식의 Jake, Frank, MJ, Zombie에 접근
        { 
            ChangeLayer(this.transform.GetChild(i).gameObject, 7); // 플레이어 레이어 설정
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
        if (pv.IsMine)
        {
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
            Renderer[] renderers = obj.transform.GetComponentsInChildren<Renderer>();

            foreach (var renderer in renderers)
            {
                int layerNumber = renderer.gameObject.layer == 14 ? 0 : layer;
                // 렌더 게임오브젝트의 레이어가 14(fireaxe)였다면 default로 변경
                // 아니면 layer로 변경
                renderer.gameObject.layer = layerNumber;
            }
        }
    }
}
