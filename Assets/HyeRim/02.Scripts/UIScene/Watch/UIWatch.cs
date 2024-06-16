using NHR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWatch : MonoBehaviour
{
    public UIInventory uiInventory;

    private void Awake()
    {
        this.uiInventory = GetComponentInChildren<UIInventory>();
    }

}
