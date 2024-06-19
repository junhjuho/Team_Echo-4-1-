using System;
using System.Collections;
using System.Collections.Generic;
using Jaewook;
using JetBrains.Annotations;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Collision doorCollision;
    [Header("���� �Է�")]
    AudioSource audioSource;
    [Header("door �ִϸ��̼� �Է�")]
    public Animator animator;
    [Header("��ƼŬ �Է�")]
    public ParticleSystem particleSys;
    private void Start()
    {
        animator = GetComponent<Animator>();

        
    }

    public void OnTriggerEnter(Collider other)
    {
        // �ٵ� FinalKey�� ���ִµ� Stay�� �ǳ�? -> mesh�� collider�� ������
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

        // �� �ִϸ��̼� ���
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }

    }
}
