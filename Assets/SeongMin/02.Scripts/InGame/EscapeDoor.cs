using Jaewook;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using SeongMin;
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
    private void Start()
    {
        Invoke("OnMiniMap", 0.5f);
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
        GameDB.Instance.isWin = true;

        EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Notice_Result);
        StartCoroutine(EndCoroutine());
        
    }
    IEnumerator EndCoroutine()
    {
        yield return new WaitForSecondsRealtime(2f);
        GameDB.Instance.playerMission.WinCheck("RunnerWin");
    }
    private void OnMiniMap()
    {
        transform.Find("MinimapIcon").gameObject.SetActive(true);
    }
}
