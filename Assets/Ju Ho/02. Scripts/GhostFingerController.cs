using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GhostFingerController : MonoBehaviour
{
    public enum HandType
    {
        LeftHand, RightHand
    }

    public HandType handType = HandType.LeftHand;

    ActionBasedController controller;

    Animator animator;
    PhotonView pv;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        pv = this.GetComponent<PhotonView>();

        if (handType == HandType.LeftHand)
        {
            if (this.transform.GetParentComponent<ActionBasedController>() != null)
            {
                controller = this.GetComponentInParent<ActionBasedController>();
            }
        }
        else if (handType == HandType.RightHand)
        {
            if (this.transform.GetParentComponent<CustomActionBasedController>() != null)
            {
                controller = this.GetComponentInParent<CustomActionBasedController>();
            }
        }
    }

    private void Update()
    {
        FingerMove();
    }

    void FingerMove()
    {
        if (pv.IsMine)
        {
            float TriggerValue = controller.activateActionValue.reference.action.ReadValue<float>();
            animator.SetFloat("Trigger", TriggerValue);

            float GripValue = controller.selectActionValue.reference.action.ReadValue<float>();
            animator.SetFloat("Grip", GripValue);
        }
    }
}
