using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : PlayerMovement
{
    public override void OnEnable()
    {
        base.OnEnable();
        if(smartWatch.gameObject.activeSelf)
            smartWatch.gameObject.SetActive(false);
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