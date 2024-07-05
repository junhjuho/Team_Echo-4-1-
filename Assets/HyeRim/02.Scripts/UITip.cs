using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UITip : MonoBehaviour
{
    //[Header("기본 셋팅")]
    //public GameObject defaultPopup;

    [Header("Hands Animation")]
    public TutorialHands hands;

    [Header("팁 배열")]
    public GameObject[] tips;

    [Header("현재 팝업")]
    public GameObject prevPopup;

    private Animator animator;
    private int preIndex;

    //초기 셋팅
    public void Init()
    {
        //this.defaultPopup.SetActive(false);
        this.hands.Init();
        foreach (var tip in tips) tip.SetActive(false);
        this.animator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Popup_Tip, new EventHandler<string>((type, tipName) =>
        {
            Debug.Log("popup tip");
            this.preIndex = this.PopupTip(tipName);
            this.animator.SetTrigger("Pop");
        }));

    }
    public int PopupTip(string tipName)
    {
        this.ClosePopup();
        switch (tipName)
        {
            case "Move":
                this.tips[0].SetActive(true);
                this.hands.gameObject.SetActive(true);
                this.hands.SetAnimation("Move");
                this.hands.SetAnimation("ButtonA");
                return 0;
            case "Grab":
                this.tips[1].SetActive(true);
                this.hands.gameObject.SetActive(true);
                this.hands.SetAnimation("Grab");
                return 1;
            case "Flashlight":
                this.tips[2].SetActive(true);
                this.hands.Init();
                return 2;
            case "Map":
                this.tips[3].SetActive(true);
                this.hands.Init();
                return 3;
            case "FinalKey":
                this.hands.Init();
                this.tips[4].SetActive(true);
                return 4;
            default:
                return -1;
        }
    }
    public void ClosePopup()
    {
        //this.defaultPopup.SetActive(false);
        if (this.preIndex == 0 || this.preIndex == 1) this.hands.Init();
        this.tips[this.preIndex].SetActive(false);
    }
}
