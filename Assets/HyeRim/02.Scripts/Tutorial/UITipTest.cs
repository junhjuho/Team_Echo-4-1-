using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UITipTest : MonoBehaviour
{
    [Header("기본 셋팅")]
    public GameObject defaultPopup;

    [Header("Hands Animation")]
    public TutorialHands hands;

    [Header("팁 배열")]
    public GameObject[] tips;

    [Header("현재 팝업")]
    public GameObject nowPopup;

    public int nowIndex;

    private WaitForSeconds closeTime = new WaitForSeconds(5f);

    private void Awake()
    {
        this.Init();
    }
    //초기 셋팅
    public void Init()
    {
        Debug.Log("Init");
        this.defaultPopup.SetActive(false);
        this.hands.Init();
        foreach (var tip in tips)
        {
            tip.SetActive(false);
        }
    }

    private void Start()
    {
        StartCoroutine(this.CPopupTip("Move"));


        EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Popup_Tip, new EventHandler<string>((type, tipName) =>
        {
            Debug.Log("popup tip");
            StartCoroutine(this.CPopupTip(tipName));


        }));

    }
    public IEnumerator CPopupTip(string tipName)
    {
        Debug.Log(tipName);
        this.defaultPopup.SetActive(true);
        switch (tipName)
        {
            case "Move":
                this.tips[0].SetActive(true);
                this.hands.gameObject.SetActive(true);
                this.hands.SetAnimation("Move");
                this.hands.SetAnimation("ButtonA");
                this.nowIndex = 0;
                break;
            case "Grab":
                this.tips[1].SetActive(true);
                this.hands.gameObject.SetActive(true);
                this.hands.SetAnimation("Grab");
                this.nowIndex = 1;
                break;
            case "Flashlight":
                this.tips[2].SetActive(true);
                this.nowIndex = 2;
                break;
            case "Battery":
                this.tips[3].SetActive(true);
                this.nowIndex = 3;
                break;
            case "Map":
                this.tips[4].SetActive(true);
                this.nowIndex = 4;
                break;
            case "FinalKey":
                this.tips[5].SetActive(true);
                this.nowIndex = 5;
                break;
        }

        yield return this.closeTime;

        this.ClosePopup(this.nowIndex);
    }
    public void ClosePopup(int index)
    {
        Debug.Log("Close Popup");
        this.defaultPopup.SetActive(false);
        if (index == 0 || index == 1) this.hands.Init();
        this.tips[index].SetActive(false);
        StopAllCoroutines();
    }
}
