using NHR;
using Photon.Pun.Demo.PunBasics;
using SeongMin;
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
    public override void PlayerMove() // 플레이어 걷기 , 달리기
    {
        if (pv.IsMine)
        {
            base.PlayerMove();  // PlayerMovement 스크립트 상속

            if (scene.name == ("InGameScene 1"))
            {
                SeongMin.GameManager.Instance.playerManager.humanMovement = this;
                isEnergyDown = SeongMin.GameManager.Instance.playerManager.uiPlayer.isEnergyDown;
            }
            else // InGameScene 1이 아닐 때, 
            {
                isEnergyDown = false;
            }

            isRunBtnDown = inputActionAsset.actionMaps[4].actions[11].IsPressed(); // 달리기 버튼

            float moveBlendtree = isRunBtnDown && !isEnergyDown ? 1f : 0.5f; // 달리기 버튼에 따른 블렌드 트리

            moveProvider.moveSpeed = isRunBtnDown && !isEnergyDown ? 10f : 5f; // 달리기 버튼에 따른 속도
            
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

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (pv.IsMine && TryGetComponent(out MonsterMovement monsterMovement))
        {
            Vector3 zombiePos = other.transform.position - this.transform.position;
            zombiePos.Normalize();
            float attackPos = Vector3.Dot(this.transform.forward, zombiePos);

            if (attackPos > 0)
            {
                animator.SetTrigger("Forward Die");
            }
            else
            {
                animator.SetTrigger("Backward Die");
            }

            var heart = SeongMin.GameManager.Instance.playerManager.heart;
            EventDispatcher.instance.SendEvent<int>((int)NHR.EventType.eEventType.Notice_Attacked, heart);
            heart--;
        }
    }
}
