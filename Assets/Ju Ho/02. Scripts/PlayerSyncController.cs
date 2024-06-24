using Photon.Pun;
using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlayerSyncController : MonoBehaviour
{
    public PhotonView pv;
    public RiggingManager riggingManager;

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    // xr origin�� ���, �޼�, ������
    public XROrigin origin;
    Transform headRig;
    Transform leftHandRig;
    Transform rightHandRig;

    public AudioSource audioSource;
    public AudioClip footStepSound;
    public AudioClip[] zombieSound;
    public GameObject bloodObject;
    public GameObject bloodPoint;

    
    void Start()
    {
        for (int i = 0; i < 4; i++) // �÷��̾� ������ �ڽ��� Jake, Frank, MJ, Zombie�� ����
        { 
            ChangeLayer(this.transform.GetChild(i).gameObject, 7); // �÷��̾� ���̾� ����
        }

        if(pv.IsMine)
        {
            origin = FindObjectOfType<XROrigin>();
            headRig = origin.transform.GetChild(0).GetChild(0);      // xr origin / camera offset / main camera
            leftHandRig = origin.transform.GetChild(0).GetChild(1);  // xr origin / camera offset / left controller
            rightHandRig = origin.transform.GetChild(0).GetChild(2); // xr origin / camera offset / right controller
        }
    }

    void Update()
    {
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
                // ���� ���ӿ�����Ʈ�� ���̾ 14(Fireaxe)���ٸ� default�� ����
                // �ƴϸ� layer�� ����
                renderer.gameObject.layer = layerNumber;
            }
        }
    }

    public void PlayFootStepSound()
    {
        if(pv.IsMine && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(footStepSound);
            pv.RPC("PhotonFootStepSound", RpcTarget.Others);
        }
    }

    [PunRPC]
    public void PhotonFootStepSound()
    {
        audioSource.PlayOneShot(footStepSound);
    }

    public void ZombieSound()
    {
        if (!audioSource.isPlaying)
        {
            int i = Random.Range(1, zombieSound.Length);
            pv.RPC("PhotonZombieSound", RpcTarget.Others, i);
        }
    }

    [PunRPC]
    public void PhotonZombieSound(int index)
    {
        audioSource.PlayOneShot(zombieSound[index]);
    }

    public void BloodEffect(Collider other)
    {
        pv.RPC("PhotonBloodEffect", RpcTarget.All, other);
    }

    [PunRPC]
    public void PhotonBloodEffect(Collider other)
    {
        var effectPos = this.transform.position + Vector3.up * 1.27f;
        Instantiate(bloodObject, effectPos, Quaternion.identity);
    }
}
