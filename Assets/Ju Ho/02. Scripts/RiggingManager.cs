using Photon.Pun;
using UnityEngine;

public class RiggingManager : MonoBehaviour
{
    public Transform leftHandIK;
    public Transform rightHandIK;
    public Transform headIK;

    public Transform leftHandController;
    public Transform rightHandController;
    public Transform hmd;

    public Vector3[] leftOffset;
    public Vector3[] rightOffset;
    public Vector3[] headOffset;

    public float smoothValue = 0.1f;
    public float modelHeight = 1.67f;

    PhotonView pv;

    private void Start()
    {
        pv = this.GetComponentInParent<PhotonView>();
    }
    void LateUpdate()
    {
        if (pv.IsMine)
        {
            MappingHandTransform(leftHandIK, leftHandController, true);
            MappingHandTransform(rightHandIK, rightHandController, false);
            MappingBodyTransform(headIK, hmd);
            MappingHeadTransform(headIK, hmd);
        }
    }

    void MappingHandTransform(Transform ik, Transform controller, bool isLeft) // 핸드 컨트롤러 동기화
    {
        var offset = isLeft ? leftOffset : rightOffset;
        ik.position = controller.TransformPoint(offset[0]);
        ik.rotation = controller.rotation * Quaternion.Euler(offset[1]);
    }

    void MappingBodyTransform(Transform ik, Transform hmd) // 헤드와 몸통 동기화
    {
        this.transform.position = new Vector3(hmd.position.x, hmd.position.y - modelHeight, hmd.position.z);
        if (hmd.position.y < modelHeight) 
        {
            this.transform.position = new Vector3(hmd.position.x, 0, hmd.position.z);
        }
        float yaw = hmd.eulerAngles.y;
        var targetRotation = new Vector3(this.transform.eulerAngles.x, yaw, this.transform.eulerAngles.z);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(targetRotation), smoothValue);
    }

    void MappingHeadTransform(Transform ik, Transform hmd) // 헤드 동기화
    {
        ik.position = hmd.TransformPoint(headOffset[0]);
        ik.rotation = hmd.rotation * Quaternion.Euler(headOffset[1]);
    }
}
