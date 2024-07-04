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

    void OnDisable() // 캐릭터 오브젝트 비활성화될 시
    {
        if (isDie && pv.IsMine)
        {
            SeongMin.GameManager.Instance.playerManager.heart--;
            print("현재 피 " + SeongMin.GameManager.Instance.playerManager.heart);

            // 체력이 1보다 낮으면
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

    public void OnHit(Collider other) // 때린 물체가 Fireaxe라면 오브젝트 비활성화, OnDisable실행
    {
        if (pv.IsMine && other.CompareTag("Fireaxe") && isDie == false)
        {
            isDie = true;

            //공격 UI 이벤트
            //EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Attack);

            this.gameObject.SetActive(false);
        }
    }
}
