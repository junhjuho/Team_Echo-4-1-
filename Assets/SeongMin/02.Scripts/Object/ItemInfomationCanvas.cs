using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfomationCanvas : MonoBehaviour
{
    public GameObject image;
    public TMP_Text text;

    private void Awake()
    {
        GameDB.Instance.itemInfomationCanvas = this;
    }
    private void Start()
    {
        image = transform.Find("Image").gameObject;
        text = transform.Find("Text").GetComponent<TMP_Text>();
    }
}
