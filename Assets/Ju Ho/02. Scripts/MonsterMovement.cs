using SeongMin;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MonsterMovement : PlayerMovement
{
    public GameObject origin;
    public PlayerAction[] controllers;

    void OnEnable()
    {
        origin = FindObjectOfType<XROrigin>().gameObject;
        controllers = origin.GetComponentsInChildren<PlayerAction>();
        
        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].transform.GetChild(1).gameObject.SetActive(true);
        }
    }


    private void Update()
    {
        PlayerMove();
    }

    public override void PlayerMove() // °È±â 
    {
        if (pv.IsMine)
        {
            base.PlayerMove();
            moveProvider.moveSpeed = 4f;
            animator.SetFloat("Walk", dir.magnitude); 
        }
    }
}