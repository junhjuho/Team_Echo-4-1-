using Photon.Pun;
using SeongMin;
using System;
using Unity.XR.CoreUtils;
using UnityEngine;

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
        PlayerMove();
    }

    public override void PlayerMove() // �ȱ� 
    {
        if (pv.IsMine)
        {
            base.PlayerMove();
            moveProvider.moveSpeed = 3f;
            animator.SetFloat("Walk", Convert.ToInt32(isMove));
        }
    }

    public void MosterSound() // ������
    {
        playerSyncController.ZombieSound();
    }
}