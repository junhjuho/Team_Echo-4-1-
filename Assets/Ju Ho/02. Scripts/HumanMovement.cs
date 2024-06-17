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
    public override void PlayerMove() // 걷기와 달리기 
    {
        if (pv.IsMine)
        {
            base.PlayerMove();  // PlayerMovement의 버튼 입력 이벤트를 상속받음

            if (scene.name == ("InGameScene 1"))
            {
                SeongMin.GameManager.Instance.playerManager.humanMovement = this;
                isEnergyDown = SeongMin.GameManager.Instance.playerManager.uiPlayer.isEnergyDown;
            }
            else // 아니라면
            {
                isEnergyDown = false;
            }

            isRunBtnDown = inputActionAsset.actionMaps[4].actions[11].IsPressed(); // 달리기 버튼 입력 이벤트

            float moveBlendtree = isRunBtnDown && !isEnergyDown ? 1f : 0.5f; // 애니메이션 블렌드 트리

            moveProvider.moveSpeed = isRunBtnDown && !isEnergyDown ? 10f : 5f; // 걷기 , 달리기 속도
            
            animator.SetFloat("Move", dir.magnitude * moveBlendtree);
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

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(/*pv.IsMine &&*/ other.transform.root.GetChild(3).GetComponent<MonsterMovement>() != null)
        {
            Vector3 zombiePos = other.transform.position - this.transform.position;
            zombiePos.Normalize();
            float attackPos = Vector3.Dot(this.transform.forward, zombiePos);

            if(attackPos > 0)
            {
                animator.SetTrigger("Forward Die");
                Debug.Log("인간이 좀비 앞에 있따");
            }
            else
            {
                animator.SetTrigger("Backward Die");
                Debug.Log("인간이 좀비 뒤에 있다");
            }

            for(int i = 0; i < 3; i++)
            {
                SeongMin.GameManager.Instance.playerManager.uiPlayer.imageDeaths[i].gameObject.SetActive(true);
            }

            Debug.Log("충돌");
            //animator.SetBool("Die", true);
        }
    }
}
