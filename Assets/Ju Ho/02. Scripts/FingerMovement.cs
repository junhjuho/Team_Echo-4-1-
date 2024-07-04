using Photon.Pun;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FingerMovement : PlayerMovement
{
    public ActionBasedController[] controllers;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

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
