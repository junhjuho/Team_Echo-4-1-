using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static Action action;

    private Move playerMove;
    private Turn playerTurn;

    public PlayerControl()
    {
        this.playerMove = new Move();
        this.playerTurn = new Turn();
    }

    void Start()
    {
        action += playerMove.Function;
        action += playerTurn.Function;

    }

    void Update()
    {
        action.Invoke();
    }

}
