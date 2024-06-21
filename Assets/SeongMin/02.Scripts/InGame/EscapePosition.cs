using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePosition : MonoBehaviour
{
    private void Awake()
    {
        GameDB.Instance.escapeDoorPositionList.Add(this.transform);
    }
}
