using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset inputActionAsset;

    [SerializeField] protected ActionBasedContinuousMoveProvider moveProvider;
    [SerializeField] protected Animator animator;
    [SerializeField] protected PhotonView pv;

    protected Vector2 dir;

    public virtual void Start()
    {
        animator = this.GetComponent<Animator>();
        pv = this.transform.GetComponentInParent<PhotonView>();
        moveProvider = FindObjectOfType<ActionBasedContinuousMoveProvider>();
        inputActionAsset = Resources.Load<InputActionAsset>("XRI Default Input Actions");
    }


    public virtual void PlayerMove() // 걷기 입력 이벤트
    {
        Vector2 _movePosition = inputActionAsset.actionMaps[3].actions[5].ReadValue<Vector2>();

        dir = new Vector2(_movePosition.x, _movePosition.y).normalized;
    }
}
    //public bool isGround;
    //public InputActionReference jump;
    //public Rigidbody rb;
    //public bool isJump;
    //float jumpPower = 2f;    

    //public virtual void Jump() // 점프
    //{
    //    if (pv.IsMine)
    //    {
    //        isJump = inputActionAsset.actionMaps[4].actions[12].IsPressed(); // 점프 입력 이벤트

    //        if (isJump && isGround)
    //        {
    //            Debug.Log(rb.velocity.y);
    //            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    //            Debug.Log("점프");
    //        }
    //        else
    //        {
    //            Debug.Log("착지");
    //        }
    //    }
    //    else
    //        return;
    //}
