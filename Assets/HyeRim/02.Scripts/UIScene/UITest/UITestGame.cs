using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITestGame : MonoBehaviour
{
    [Header("���� ���� - ������")]
    public Button btnAttacked;  //���� ����

    [Header("Ż�� ����")]
    public Button btnEnd;   //Ż�� ����, ���� ���

    [Header("���� ���� - ������")]
    public Button btnChangeMonster; //���� ����, �����ڶ��

    public int heart = 3;
    private void Start()
    {
        this.heart = 3;
        /*
        this.btnAttacked.onClick.AddListener(() =>
        {
            EventDispatcher.instance.SendEvent<int>((int)NHR.EventType.eEventType.Notice_Attacked, this.heart);
            this.heart--;
        });
        this.btnEnd.onClick.AddListener(() =>
        {
            Debug.Log("Ż�� ����");
            GameDB.Instance.isWin = true;
            EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Watching_Game);
        });
        this.btnChangeMonster.onClick.AddListener(() =>
        {
            Debug.Log("���� ����");
            EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Change_Monster);
        });
        */
    }
}
