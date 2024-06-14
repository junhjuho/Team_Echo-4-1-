using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanMovement : PlayerMovement
{
    public InputActionProperty leftPinch;
    public InputActionProperty leftGrip;

    public InputActionProperty rightPinch;
    public InputActionProperty rightGrip;

    public bool isKneeling;

    bool _isRun;

    public override void Start()
    {
        base.Start();

    }

    void Update()
    {
        PlayerMove();
        FingerMove();
        Kneeling();
    }
    public override void PlayerMove() // �ȱ�� �޸��� 
    {
        if (pv.IsMine)
        {
            base.PlayerMove();  // PlayerMovement�� ��ư �Է� �̺�Ʈ�� ��ӹ���

            _isRun = inputActionAsset.actionMaps[4].actions[11].IsPressed(); // �޸��� ��ư �Է� �̺�Ʈ

            moveProvider.moveSpeed = _isRun ? 2f : 1f;

            animator.SetFloat("Move", dir.magnitude * moveProvider.moveSpeed);
        }
        else
            return;
    }

    void Kneeling()
    {
        isKneeling = inputActionAsset.actionMaps[4].actions[12].IsPressed();
        bool _KneelingAnim = isKneeling ? true : false;

        animator.SetBool("isKneel", _KneelingAnim);
    }


    public void FingerMove() // �հ��� ������
    {
        if (pv.IsMine)
        {
            float leftTriggerValue = leftPinch.action.ReadValue<float>();
            animator.SetFloat("Left Trigger", leftTriggerValue);

            float leftGripValue = leftGrip.action.ReadValue<float>();
            animator.SetFloat("Left Grip", leftGripValue);

            float rightTriggerValue = rightPinch.action.ReadValue<float>();
            animator.SetFloat("Right Trigger", rightTriggerValue);

            float rightGripValue = rightGrip.action.ReadValue<float>();
            animator.SetFloat("Right Grip", rightGripValue);
        }
        else
            return;
    }
}
