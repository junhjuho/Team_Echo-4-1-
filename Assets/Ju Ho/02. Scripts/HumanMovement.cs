using NHR;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization; 
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HumanMovement : PlayerMovement
{
    public bool isRunBtnDown;
    UIPlayer uiPlayer;
    Scene scene;
    bool isEnergyDown;

    public override void Start()
    {
        base.Start();

        scene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        PlayerMove();
        FingerMove(animator);
    }
    public override void PlayerMove() // �ȱ�� �޸��� 
    {
        if (pv.IsMine)
        {
            base.PlayerMove();  // PlayerMovement�� ��ư �Է� �̺�Ʈ�� ��ӹ���

            if (scene.name == ("InGameScene 1"))
            {
                SeongMin.GameManager.Instance.playerManager.humanMovement = this;
                isEnergyDown = SeongMin.GameManager.Instance.playerManager.uiPlayer.isEnergyDown;
            }
            else // �ƴ϶��
            {
                isEnergyDown = false;
            }

            isRunBtnDown = inputActionAsset.actionMaps[4].actions[11].IsPressed(); // �޸��� ��ư �Է� �̺�Ʈ

            float moveBlendtree = isRunBtnDown && !isEnergyDown ? 1f : 0.5f; // �ִϸ��̼� ���� Ʈ��

            moveProvider.moveSpeed = isRunBtnDown && !isEnergyDown ? 10f : 5f; // �ȱ� , �޸��� �ӵ�
            
            animator.SetFloat("Move", dir.magnitude * moveBlendtree);
        }
        else
            return;
    }

    public void FingerMove(Animator animator) // �հ��� ������
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
