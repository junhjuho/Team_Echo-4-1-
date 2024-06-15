using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanMovement : PlayerMovement
{
    public bool isKneeling;

    bool _isRun;

    public override void Start()
    {
        base.Start();
    }

    void Update()
    {
        PlayerMove();
        FingerMove(animator);
    }
    public override void PlayerMove() // 걷기와 달리기 
    {
        if (pv.IsMine)
        {
            base.PlayerMove();  // PlayerMovement의 버튼 입력 이벤트를 상속받음

            _isRun = inputActionAsset.actionMaps[4].actions[11].IsPressed(); // 달리기 버튼 입력 이벤트

            moveProvider.moveSpeed = _isRun ? 2f : 1f;

            animator.SetFloat("Move", dir.magnitude * moveProvider.moveSpeed);
        }
        else
            return;
    }

    public void FingerMove(Animator animator) // 손가락 움직임
    {
        if (pv.IsMine)
        {
            float leftTriggerValue = inputActionAsset.actionMaps[2].actions[3].ReadValue<float>();
            animator.SetFloat("Left Trigger", leftTriggerValue);

            float leftGripValue = inputActionAsset.actionMaps[2].actions[1].ReadValue<float>();
            animator.SetFloat("Left Grip", leftGripValue);

            float rightTriggerValue = inputActionAsset.actionMaps[5].actions[3].ReadValue<float>();
            animator.SetFloat("Right Trigger", rightTriggerValue);

            float rightGripValue = inputActionAsset.actionMaps[5].actions[1].ReadValue<float>();
            animator.SetFloat("Right Grip", rightGripValue);
        }
        else
            return;
    }
}
