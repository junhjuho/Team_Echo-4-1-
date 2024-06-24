using NHR;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovement : MonoBehaviour
{
    protected Animator animator;
    protected InputActionAsset inputActionAsset;
    protected ActionBasedContinuousMoveProvider moveProvider;
    protected PlayerSyncController playerSyncController;
    protected PhotonView pv;
    
    protected Vector2 dir;
    protected Vector2 movePosition;
    protected bool isMove;

    public virtual void Start()
    {
        animator = this.GetComponent<Animator>();
        pv = this.transform.GetComponentInParent<PhotonView>();
        playerSyncController = this.transform.GetComponentInParent<PlayerSyncController>();
        moveProvider = FindObjectOfType<ActionBasedContinuousMoveProvider>();
        inputActionAsset = Resources.Load<InputActionAsset>("XRI Default Input Actions");
    }

    public virtual void PlayerMove() // 걷기 입력 이벤트
    {
        movePosition = inputActionAsset.actionMaps[3].actions[5].ReadValue<Vector2>();
        isMove = inputActionAsset.actionMaps[3].actions[5].IsPressed();

        dir = new Vector2(movePosition.x, movePosition.y).normalized;
    }
}
