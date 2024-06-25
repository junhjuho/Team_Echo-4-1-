using Jaewook;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeDoor : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out FinalKey finalKey))
        {
            animator.SetTrigger("isOpen");
            Invoke("GameEnding", 1f);
        }
    }
    private void GameEnding()
    {

        SeongMin.GameDB.Instance.isWin = true;
        SeongMin.GameDB.Instance.hasGameData = true;

        SeongMin.GameManager.Instance.roundManager.RoundPlayerDataReset();
        //TODO �������� �¸� �̹Ƿ� �¸� �̺�Ʈ ���� 
        PhotonNetwork.LoadLevel("LobbyScene 1");
    }
}
