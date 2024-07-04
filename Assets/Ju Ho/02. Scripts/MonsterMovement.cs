using Photon.Pun;
using SeongMin;
using System;
using Unity.XR.CoreUtils;
using UnityEngine;

public class MonsterMovement : PlayerMovement, IMovable
{
    public GameObject origin;
    public PlayerAction[] controllers;
    void OnEnable()
    {
        origin = FindObjectOfType<XROrigin>().gameObject;
        controllers = origin.GetComponentsInChildren<PlayerAction>();

        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void OnDisable()
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public override void Start()
    {
        base.Start();
        //playerSyncController.ZombieSound(0); // ������ �� �ѹ�
    }
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (pv.IsMine)
        {
            move = moveProvider.leftHandMoveAction.reference.action.ReadValue<Vector2>();
            moveProvider.moveSpeed = 3f;
            animator.SetFloat("Walk", move.magnitude);
        }
    }

    public void MosterSound() // ������
    {
        playerSyncController.ZombieSound();
    }
}