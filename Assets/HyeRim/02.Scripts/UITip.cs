using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITip : MonoBehaviour
{
    [Header("기본 셋팅")]
    public GameObject defaultPopup;

    [Header("Hands Animation")]
    public TutorialHands hands;

    [Header("팁 배열")]
    public GameObject[] tips;

    [Header("현재 팝업")]
    public GameObject nowPopup;

    //초기 셋팅
    public void Init()
    {
        this.defaultPopup.SetActive(false);
        this.hands.Init();
        foreach (var tip in tips) tip.SetActive(false);
    }

    private void Start()
    {
        EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Popup_Tip, new EventHandler<string>((type, tipName) =>
        {
            Debug.Log("popup tip");
            int index = this.PopupTip(tipName);
            Invoke("this.ClosePopup", 3f);
        }));

    }
    public int PopupTip(string tipName)
    {
        this.defaultPopup.SetActive(true);
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
                return 2;
            case "Battery":
                this.tips[3].SetActive(true);
                return 3;
            case "Map":
                this.tips[4].SetActive(true);
                return 4;
            case "FinalKey":
                this.tips[5].SetActive(true);
                return 5;
            default:
                return -1;
        }
    }
    public void ClosePopup(int index)
    {
        if (index == 0 || index == 1) this.hands.Init();
        this.tips[index].SetActive(false);
    }
}
