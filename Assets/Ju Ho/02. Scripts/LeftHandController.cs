using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeftHandController : MonoBehaviour
{
    private Animator anim;

    public InputActionProperty leftPinch;
    public InputActionProperty leftGrip;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float leftTriggerValue = leftPinch.action.ReadValue<float>();
        anim.SetFloat("Trigger", leftTriggerValue);

        float leftGripValue = leftGrip.action.ReadValue<float>();
        anim.SetFloat("Grip", leftGripValue);
    }
}
