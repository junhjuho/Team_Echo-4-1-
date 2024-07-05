using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    public TMP_Text textTimer;
    public GameObject clockSound;

    private void Awake()
    {
        this.textTimer = GetComponentInChildren<TMP_Text>();
        this.clockSound.SetActive(false);
    }
    public void UpdateTimer(int sec)
    {
        int min = 0;
        if (sec > 60)
        {
            min = sec / 60;
            sec %= 60;
        }
        else this.clockSound.SetActive(true);
        this.textTimer.text = string.Format("남은 시간 : {0:D2}:{1:D2}", min, sec);
    }

}
