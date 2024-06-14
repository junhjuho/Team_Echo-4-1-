using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightHandController : MonoBehaviour
{
    private Animator anim;

    public InputActionProperty RightPinch;
    public InputActionProperty RightGrip;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float RightTriggerValue = RightPinch.action.ReadValue<float>();
        anim.SetFloat("Trigger", RightTriggerValue);

        float RightGripValue = RightGrip.action.ReadValue<float>();
        anim.SetFloat("Grip", RightGripValue);
    }
}
