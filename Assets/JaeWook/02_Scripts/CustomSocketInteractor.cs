//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

//public class CustomSocketInteractor : XRSocketInteractor
//{
//    protected override void Start()
//    {
//        this.selectEntered.AddListener(SelectEnter);
//        this.selectExited.AddListener(SelectExit);
        
//    }

//    private void SelectEnter(SelectEnterEventArgs args)
//    {
//        // mesh renderer -> 3D ȭ�� �� �׸�
//        args.interactableObject.transform.GetComponent<MeshRenderer>().enabled = false;   
//    }
//    private void SelectExit(SelectExitEventArgs args)
//    {
//        args.interactableObject.transform.GetComponent<MeshRenderer>().enabled = true;   
        
//    }
//}
