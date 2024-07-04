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

    protected ActionBasedController controller;

    public PhotonView pv;

    protected Vector2 dir;
    protected Vector2 movePosition;
    protected Vector2 move;
    protected bool isMove;

    public virtual void Start()
    {
        animator = this.GetComponent<Animator>();
        pv = this.transform.GetComponentInParent<PhotonView>();
        moveProvider = FindObjectOfType<ActionBasedContinuousMoveProvider>();
        //inputActionAsset = Resources.Load<InputActionAsset>("XRI Default Input Actions");
    }
}
