using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

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

    Transform lefttHandIK_hint;
    Transform rightHandIK_hint;


    RaycastHit hitInfo;

    void Start()
    {
        pv = this.GetComponent<PhotonView>();

        ChangeLayer(this.gameObject, 7); // 플레이어 레이어 설정
        if(pv.IsMine)
        {
            XROrigin origin = FindObjectOfType<XROrigin>();
            headRig = origin.transform.GetChild(0).GetChild(0);      // xr origin / camera offset / main camera
            leftHandRig = origin.transform.GetChild(0).GetChild(1);  // xr origin / camera offset / left controller
            rightHandRig = origin.transform.GetChild(0).GetChild(2); // xr origin / camera offset / right controller
            riggingManager = this.GetComponentInChildren<RiggingManager>();

            if (this.transform.GetComponentInChildren<HumanMovement>() != null)
            {
                humanMovement = this.GetComponentInChildren<HumanMovement>();
            }
            else
            {
                monsterMovement = this.GetComponentInChildren<MonsterMovement>();
            }
            lefttHandIK_hint = this.transform.GetChild(0).GetChild(9).GetChild(0).GetChild(1);
            rightHandIK_hint = this.transform.GetChild(0).GetChild(9).GetChild(1).GetChild(1);
        }

        //if (this.transform.name == "Player")
        //{
        //    riggingManager = GameObject.Find("Banana Man").GetComponent<RiggingManager>();
        //}
        //else
        //{
        //    riggingManager = GameObject.Find("skinless zombie").GetComponent<RiggingManager>();
        //}
    }

    void Update()
    {
        //Debug.DrawRay(head.transform.position, - head.transform.up * 5f, Color.red);

        if (pv.IsMine)
        {
            if (headRig.transform.position.y > riggingManager.modelHeight)
            {
                headRig.transform.position = new Vector3(headRig.transform.position.x, riggingManager.modelHeight, headRig.transform.position.z);
                //Debug.Log(headRig.transform.position);
                head.transform.position = new Vector3(head.transform.position.x, riggingManager.modelHeight, head.transform.position.z);
            }

            if (humanMovement != null && humanMovement.isKneeling)
            {
                headRig.transform.position = new Vector3(headRig.transform.position.x, 1f, headRig.transform.position.z);
                leftHandRig.transform.position = new Vector3(leftHandRig.transform.position.x, leftHandRig.transform.position.y - 0.67f, leftHandRig.transform.position.z);
                leftHand.transform.position = new Vector3(leftHand.transform.position.x, leftHand.transform.position.y - 0.67f, leftHand.transform.position.z);
                rightHandRig.transform.position = new Vector3(rightHandRig.transform.position.x, rightHandRig.transform.position.y - 0.67f, rightHandRig.transform.position.z);
                rightHand.transform.position = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y - 0.67f, rightHand.transform.position.z);

                lefttHandIK_hint.transform.position = new Vector3(lefttHandIK_hint.transform.position.x, lefttHandIK_hint.transform.position.y - 0.67f, lefttHandIK_hint.transform.position.z);
                rightHandIK_hint.transform.position = new Vector3(rightHandIK_hint.transform.position.x, rightHandIK_hint.transform.position.y - 0.67f, rightHandIK_hint.transform.position.z);
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
        obj.layer = layer;

        if(pv.IsMine)
        {
            foreach(Transform child in obj.transform)
            {
                if (child.gameObject.name == "Hand_Left" || child.gameObject.name == "Hand_Right")
                    ChangeLayer(child.gameObject, 0);
                else
                    ChangeLayer(child.gameObject, layer);
            }
        }
    }
}
