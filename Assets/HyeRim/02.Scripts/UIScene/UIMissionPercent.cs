using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMissionPercent : MonoBehaviour
{
    public Image imageNotice;
    public TMP_Text textNotice;

    public void Init()
    {
        this.textNotice.text = "";
    }

}
