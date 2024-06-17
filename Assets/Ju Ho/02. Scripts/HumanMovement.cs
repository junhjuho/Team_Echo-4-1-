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
                Debug.Log("�ΰ��� ���� �տ� �ֵ�");
            }
            else
            {
                animator.SetTrigger("Backward Die");
                Debug.Log("�ΰ��� ���� �ڿ� �ִ�");
            }

            for(int i = 0; i < 3; i++)
            {
                SeongMin.GameManager.Instance.playerManager.uiPlayer.imageDeaths[i].gameObject.SetActive(true);
            }

            Debug.Log("�浹");
            //animator.SetBool("Die", true);
        }
    }
}
