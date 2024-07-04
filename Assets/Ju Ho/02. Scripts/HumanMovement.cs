using DG.Tweening;
using NHR;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using SeongMin;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HumanMovement : PlayerMovement, IMovable
{
    CustomActionBasedController customActionBasedController;
    public bool isEnergyDown = false;
    public bool isDie;
    public bool isRunBtnDown;

    public void OnEnable()
    {
        if (pv.IsMine && SeongMin.GameManager.Instance.playerManager != null)
            SeongMin.GameManager.Instance.playerManager.humanMovement = this;
    }

    public override void Start()
    {
        base.Start();

        customActionBasedController = FindAnyObjectByType<CustomActionBasedController>();

        if (pv.IsMine && SeongMin.GameManager.Instance.playerManager !=null)
            SeongMin.GameManager.Instance.playerManager.humanMovement = this;
    }

    //void OnDisable() // 캐릭터 오브젝트 비활성화될 시
    //{
    //    if (isDie && pv.IsMine)
    //    {
    //        SeongMin.GameManager.Instance.playerManager.heart--;
    //        print("현재 피 " + SeongMin.GameManager.Instance.playerManager.heart);

    //        // 체력이 1보다 낮으면
    //        if (SeongMin.GameManager.Instance.playerManager.heart <= 0)
    //        {
    //            GameDB.Instance.isWin = false;

    //            EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Notice_Result);
    //            EndCoroutine();
    //        }
    //        else
    //        {
    //            EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Notice_Attacked);

    //            GameDB.Instance.playerMission.RunnerSetActive();
    //        }
    //    }
    //}
    //private void EndCoroutine()
    //{
    //    GameDB.Instance.playerMission.WinCheck("ChaserWin");
    //}

    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (pv.IsMine)
        {
            move = moveProvider.leftHandMoveAction.reference.action.ReadValue<Vector2>();

            isRunBtnDown = customActionBasedController.runAction.reference.action.IsPressed();

            float moveBlendtree = isRunBtnDown && !isEnergyDown ? 1f : 0.5f; // 달리기 버튼에 따른 블렌드 트리

            moveProvider.moveSpeed = isRunBtnDown && !isEnergyDown ? 4f : 2f; // 달리기 버튼에 따른 속도

            Debug.Log(move.magnitude);
            animator.SetFloat("Move", move.magnitude * moveBlendtree);
        }
        else
            return;
    }



    //public void OnTriggerEnter(Collider other)
    //{
    //    OnHit(other);
    //}

    //public void OnHit(Collider other) // 때린 물체가 Fireaxe라면 오브젝트 비활성화, OnDisable실행
    //{
    //    if (pv.IsMine && other.CompareTag("Fireaxe") && isDie == false)
    //    {
    //        Debug.Log("충돌 오브젝트 : " + other.name);
    //        playerSyncController.BloodEffect(other);
    //        isDie = true;

    //        //공격 UI 이벤트
    //        //EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Attack);

    //        this.gameObject.SetActive(false);
    //    }
    //}

    void PlayFootStepSound()
    {
        playerSyncController.PlayFootStepSound();
    }
}
