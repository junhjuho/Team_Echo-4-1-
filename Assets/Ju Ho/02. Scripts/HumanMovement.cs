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

public class HumanMovement : PlayerMovement, IDamageable
{
    public GameObject FireAxe;
    public DieAnimation[] dieAnims;

    public bool isRunBtnDown;

    bool isEnergyDown;
    public bool isDie;

    private WaitForSeconds reviveTime = new WaitForSeconds(4f);

    Scene scene;

    public void OnEnable()
    {
        isDie = false;
        moveProvider.enabled = true;
    }

    public override void Start()
    {
        base.Start();
        scene = SceneManager.GetActiveScene();
        playerSyncController = this.GetComponentInParent<PlayerSyncController>();
        if (pv.IsMine)
            SeongMin.GameManager.Instance.playerManager.humanMovement = this;
    }

    void OnDisable() // 
    {
        if (isDie && pv.IsMine)
        {
            for (int i = 0; i < dieAnims.Length; i++)
            {
                if (this.gameObject.name + " Die Model" == dieAnims[i].gameObject.name) // 현재 오브젝트의 이름과 모델 애니메이션 오브젝트 이름이 같으면
                {
                    dieAnims[i].transform.gameObject.SetActive(true); // 모델 애니메이션 오브젝트를 활성화 시키고 애니메이션 실행
                    dieAnims[i].PlayerDieAnimation("Backward Die");
                    break;
                }
            }
            SeongMin.GameManager.Instance.playerManager.heart--;

            // 체력이 1보다 낮으면
            if (SeongMin.GameManager.Instance.playerManager.heart <= 0)
            {
                SeongMin.GameDB.Instance.hasGameData = true;
                SeongMin.GameDB.Instance.isWin = false;
                SeongMin.GameManager.Instance.roundManager.photonView.RPC("AllPlayerLobbySceneLoad", RpcTarget.MasterClient);
            }
            else
            {
                EventDispatcher.instance.SendEvent<int>((int)NHR.EventType.eEventType.Notice_Attacked, SeongMin.GameManager.Instance.playerManager.heart);

                StartCoroutine(this.RespawnPlayer());
            }
        }
    }

    void Update()
    {
        PlayerMove();
        FingerMove();
    }

    public override void PlayerMove() // 플레이어 걷기 , 달리기
    {
        if (pv.IsMine)
        {
            base.PlayerMove();  // PlayerMovement 스크립트 상속

            isRunBtnDown = inputActionAsset.actionMaps[4].actions[11].IsPressed(); // 달리기 버튼

            float moveBlendtree = isRunBtnDown && !isEnergyDown ? 1f : 0.5f; // 달리기 버튼에 따른 블렌드 트리

            moveProvider.moveSpeed = isRunBtnDown && !isEnergyDown ? 10f : 5f; // 달리기 버튼에 따른 속도

            animator.SetFloat("Move", dir.magnitude * moveBlendtree);
        }
        else
            return;
    }

    public void FingerMove() // 손가락 애니메이션
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

    //검토 필요
    private IEnumerator RespawnPlayer()
    {
        yield return reviveTime;
        GameDB.Instance.playerController.photonView.RPC("CharacterRePosition", RpcTarget.All);
        playerSyncController.origin.transform.position =
            SeongMin.GameManager.Instance.inGameMapManager.playerSpawnPositionList[0].position;
        StopAllCoroutines();
    }

    public void OnTriggerEnter(Collider other)
    {
        OnHit(other);
    }
    public void OnHit(Collider other) // 때린 물체가 Fireaxe라면 오브젝트 비활성화, OnDisable실행
    {
        if (pv.IsMine && other.gameObject.name == "Fireaxe")
        {
            Debug.Log("충돌 오브젝트 : " + other.name);
            playerSyncController.BloodEffect(other);
            isDie = true;
            moveProvider.enabled = false;
            this.gameObject.SetActive(false);
        }
    }

    void PlayFootStepSound()
    {
        playerSyncController.PlayFootStepSound();
    }
}
