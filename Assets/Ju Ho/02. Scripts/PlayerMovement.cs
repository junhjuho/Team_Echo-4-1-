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
    protected ActionBasedContinuousMoveProvider moveProvider;
    protected PlayerSyncController playerSyncController;

    public PhotonView pv;

    protected Vector2 move;

    public virtual void Start()
    {
        animator = this.GetComponent<Animator>();
        pv = this.transform.GetComponentInParent<PhotonView>();
        moveProvider = FindObjectOfType<ActionBasedContinuousMoveProvider>(); // XR Origin에 있는 컴포넌트
        playerSyncController = this.transform.GetComponentInParent<PlayerSyncController>();
    }

    public virtual void Move()
    {
        move = moveProvider.leftHandMoveAction.reference.action.ReadValue<Vector2>();
    }
}


