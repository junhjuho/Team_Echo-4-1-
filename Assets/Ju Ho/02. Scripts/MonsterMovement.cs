using SeongMin;
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
            moveProvider.moveSpeed = 3f;
            animator.SetFloat("Walk", dir.magnitude); 
        }
    }
}