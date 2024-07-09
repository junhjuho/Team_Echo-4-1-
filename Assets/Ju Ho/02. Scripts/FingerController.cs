using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FingerController : MonoBehaviour
{
    public ActionBasedController[] controllers;
    PhotonView pv;
    ActionBasedContinuousMoveProvider moveProvider;
    Animator animator;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        pv = this.transform.GetComponentInParent<PhotonView>();
        moveProvider = FindAnyObjectByType<ActionBasedContinuousMoveProvider>();

        for (int i = 0; i < controllers.Length; i++)
        {
            controllers = moveProvider.GetComponentsInChildren<ActionBasedController>();
        } // Left, Right Controller 배열에 담기
    }

    void Update()
    {
        FingerMove();
    }

    public void FingerMove() // 손가락 애니메이션
    {
        if (pv.IsMine)
        {
            float leftTriggerValue = controllers[0].activateActionValue.reference.action.ReadValue<float>();
            animator.SetFloat("Left Trigger", leftTriggerValue);

            float leftGripValue = controllers[0].selectActionValue.reference.action.ReadValue<float>();
            animator.SetFloat("Left Grip", leftGripValue);

            float rightTriggerValue = controllers[1].activateActionValue.reference.action.ReadValue<float>();
            animator.SetFloat("Right Trigger", rightTriggerValue);

            float rightGripValue = controllers[1].selectActionValue.reference.action.ReadValue<float>();
            animator.SetFloat("Right Grip", rightGripValue);
        }
        else
            return;
    }
}
