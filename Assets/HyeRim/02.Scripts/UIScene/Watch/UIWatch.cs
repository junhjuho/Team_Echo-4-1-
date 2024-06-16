using NHR;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWatch : MonoBehaviour
{
    public UIInventory uiInventory;
    public UIHeartWatch uiHeart;
    public int nowHeart;

    private void Awake()
    {
        this.uiInventory = GetComponentInChildren<UIInventory>();
        this.uiHeart = GetComponentInChildren<UIHeartWatch>();
    }
    private void Start()
    {
        EventDispatcher.instance.AddEventHandler<int>((int)NHR.EventType.eEventType.Notice_Attacked, new EventHandler<int>((type, heart) =>
        {
            //�����ڰ� �ƴϸ�
            if (!GameDB.Instance.playerMission.isChaser)
            {
                Debug.Log("<color=red>��Ʈ ����</color>");
                this.uiHeart.nowHeart = heart;
            }
        }));

    }
}
