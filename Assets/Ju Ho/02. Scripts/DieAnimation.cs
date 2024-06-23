using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAnimation : MonoBehaviour
{
    Animator animator;

    private void OnEnable()
    {
        animator = this.GetComponent<Animator>();
    }

    public void PlayerDieAnimation(string animationName)
    {
        animator.SetTrigger(animationName);
    }

    public void ModelActiveFalse()
    {
        this.gameObject.SetActive(false);
    }
}
