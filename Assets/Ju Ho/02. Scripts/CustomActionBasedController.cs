using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class CustomActionBasedController : ActionBasedController
{
    [SerializeField]
    InputActionProperty RunAction = 
        new InputActionProperty(new InputAction("Run", type: InputActionType.Button) { wantsInitialStateCheck = true });
   
    public InputActionProperty runAction
    {
        get => RunAction;
        set => SetInputActionProperty(ref RunAction, value);
    }

    void SetInputActionProperty(ref InputActionProperty property, InputActionProperty value)
    {
        if (Application.isPlaying)
            property.DisableDirectAction();

        property = value;

        if (Application.isPlaying && isActiveAndEnabled)
            property.EnableDirectAction();
    }
}
