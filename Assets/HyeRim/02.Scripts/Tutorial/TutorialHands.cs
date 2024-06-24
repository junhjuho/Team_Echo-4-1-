using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHands : MonoBehaviour
{
    public Animator leftHandAnimator;
    public Animator rightHandjAnimator;

    private void Awake()
    {
        this.Init();
    }
    public void Init()
    {
        this.leftHandAnimator.gameObject.SetActive(false);
        this.rightHandjAnimator.gameObject.SetActive(false);
    }

    public void SetAnimation(string triggerStr)
    {
        switch (triggerStr)
        {
            case "Move":
                this.leftHandAnimator.gameObject.SetActive(true);
                this.leftHandAnimator.SetTrigger(triggerStr);
                break;
            case "Turn":
                this.rightHandjAnimator.gameObject.SetActive(true);
                this.rightHandjAnimator.SetTrigger(triggerStr);
                break;
            case "Grab":
                this.leftHandAnimator.gameObject.SetActive(true);
                this.leftHandAnimator.SetTrigger(triggerStr);
                break;
            case "ButtonA":
                this.rightHandjAnimator.gameObject.SetActive(true);
                this.rightHandjAnimator.SetTrigger(triggerStr);
                break;
            case "Trigger":
                this.rightHandjAnimator.gameObject.SetActive(true);
                this.rightHandjAnimator.SetTrigger(triggerStr);
                break;
        }
    }
}
