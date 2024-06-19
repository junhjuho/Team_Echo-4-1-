using System;
using System.Collections;
using System.Collections.Generic;
using Jaewook;
using JetBrains.Annotations;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Collision doorCollision;
    [Header("사운드 입력")]
    AudioSource audioSource;
    [Header("door 애니메이션 입력")]
    public Animator animator;
    [Header("파티클 입력")]
    public ParticleSystem particleSys;
    private void Start()
    {
        animator = GetComponent<Animator>();

        
    }

    public void OnTriggerEnter(Collider other)
    {
        // 근데 FinalKey를 없애는데 Stay가 되나? -> mesh랑 collider만 지우자
        if (other.GetComponent<FinalKey>() != null)
        {
            FinalKey finalKey = other.GetComponent<FinalKey>();

            OpenDoor();
            particleSys.Play();

        }
    }

    public void OnTriggerStay(Collider other)
    {

    }

    internal void OpenDoor()
    {
        Debug.Log("Door opened!");

        // 문 애니메이션 재생
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }

    }
}
