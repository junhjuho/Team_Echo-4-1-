using Jaewook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEscapeDoor : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TutorialKey tutorialKey))
        {
            animator.SetTrigger("isOpen");
            EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Clear_TutorialQuest);
        }
    }
}
