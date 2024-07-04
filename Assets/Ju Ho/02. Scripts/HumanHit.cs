using Photon.Pun;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanHit : MonoBehaviour, IDamageable
{
    public bool isDie = false;
    PhotonView pv;

    void OnEnable()
    {
        isDie = false;

        if (pv.IsMine && SeongMin.GameManager.Instance.playerManager != null)
            SeongMin.GameManager.Instance.playerManager.humanHit = this;
    }


    void Start()
    {
        pv = this.transform.GetComponentInParent<PhotonView>();

        if (pv.IsMine && SeongMin.GameManager.Instance.playerManager != null)
            SeongMin.GameManager.Instance.playerManager.humanHit = this;

    }

    void OnDisable() // ĳ���� ������Ʈ ��Ȱ��ȭ�� ��
    {
        if (isDie && pv.IsMine)
        {
            SeongMin.GameManager.Instance.playerManager.heart--;
            print("���� �� " + SeongMin.GameManager.Instance.playerManager.heart);

            // ü���� 1���� ������
            if (SeongMin.GameManager.Instance.playerManager.heart <= 0)
            {
                GameDB.Instance.isWin = false;

                EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Notice_Result);
                EndCoroutine();
            }
            else
            {
                EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Notice_Attacked);

                GameDB.Instance.playerMission.RunnerSetActive();
            }
        }
    }

    private void EndCoroutine()
    {
        GameDB.Instance.playerMission.WinCheck("ChaserWin");
    }

    public void OnTriggerEnter(Collider other)
    {
        OnHit(other);
    }

    public void OnHit(Collider other) // ���� ��ü�� Fireaxe��� ������Ʈ ��Ȱ��ȭ, OnDisable����
    {
        if (pv.IsMine && other.CompareTag("Fireaxe") && isDie == false)
        {
            isDie = true;

            //���� UI �̺�Ʈ
            //EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Attack);

            this.gameObject.SetActive(false);
        }
    }
}
