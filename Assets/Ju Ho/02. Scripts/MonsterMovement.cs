using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : PlayerMovement
{
    private void Update()
    {
        PlayerMove();
    }
    public override void PlayerMove() // �ȱ� 
    {
        if (pv.IsMine)
        {
            base.PlayerMove();
            moveProvider.moveSpeed = 2f;
            animator.SetFloat("Walk", dir.magnitude); // ��Ʈ Ʈ�� �Ӱ谪�� 0 ~ 1�� ����
        }
    }
}


    //private void FixedUpdate()
    //{
    //    Jump();
    //}
    //public override void Jump()
    //{
    //    base.Jump();
    //}
