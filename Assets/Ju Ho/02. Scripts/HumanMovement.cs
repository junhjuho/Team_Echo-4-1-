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
    public bool isRunBtnDown;

    public void OnEnable()
    {
        if (pv.IsMine && SeongMin.GameManager.Instance.playerManager != null)
            SeongMin.GameManager.Instance.playerManager.humanMovement = this;
    }

    public override void Start()
    {
        base.Start();

        customActionBasedController = moveProvider.GetComponentInChildren<CustomActionBasedController>();

        if (pv.IsMine && SeongMin.GameManager.Instance.playerManager !=null)
            SeongMin.GameManager.Instance.playerManager.humanMovement = this;
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (pv.IsMine)
        {
            base.Move();

            isRunBtnDown = customActionBasedController.runAction.reference.action.IsPressed(); // 오른손 컨트롤러 A버튼

            float moveBlendtree = isRunBtnDown && !isEnergyDown ? 1f : 0.5f; // 달리기 버튼에 따른 블렌드 트리

            moveProvider.moveSpeed = isRunBtnDown && !isEnergyDown ? 4f : 2f; // 달리기 버튼에 따른 속도

            Debug.Log(move.magnitude);
            animator.SetFloat("Move", move.magnitude * moveBlendtree);
        }
        else
            return;
    }

    void PlayFootStepSound()
    {
        playerSyncController.PlayFootStepSound();
    }
}
