using Photon.Pun;
using SeongMin;
using System;
using Unity.XR.CoreUtils;
using UnityEngine;

public class MonsterMovement : PlayerMovement
{
    public GameObject origin;
    public PlayerAction[] controllers;

    void Awake()
    {
        origin = FindObjectOfType<XROrigin>().gameObject;
        controllers = origin.GetComponentsInChildren<PlayerAction>();
    }
    void OnEnable()
    {
        ControllerActive(false);
    }
    void OnDisable()
    {
        ControllerActive(true);
    }

    void ControllerActive(bool isActive)
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].transform.GetChild(0).gameObject.SetActive(isActive);
        }
    }

    public override void Start()
    {
        base.Start();
    }
    void Update()
    {
        Move();
    }

    public override void Move()
    {
        if (pv.IsMine)
        {
            base.Move();
            moveProvider.moveSpeed = 3f;
            animator.SetFloat("Walk", move.magnitude);
        }
        else
            return;
    }

    public void MosterSound() // ¿©·¯¹ø
    {
        playerSyncController.ZombieSound();
    }
}