using Jaewook;
using Photon.Pun;
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
            Invoke("GameEnding", 0.3f);
        }
    }
    private void GameEnding()
    {
        //TODO �������� �¸� �̹Ƿ� �¸� �̺�Ʈ ���� 
        PhotonNetwork.LoadLevel("LobbyScene 1");
    }
}
