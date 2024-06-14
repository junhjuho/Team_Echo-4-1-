using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITestGame : MonoBehaviour
{
    public Button btnAttacked;
    public Button btnEnd;

    public int heart = 3;
    private void Start()
    {
        this.heart = 3;
        this.btnAttacked.onClick.AddListener(() =>
        {
            EventDispatcher.instance.SendEvent<int>((int)NHR.EventType.eEventType.Notice_Attacked, this.heart);
            this.heart--;
        });
    }
}
