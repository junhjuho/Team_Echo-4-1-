using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XROriginBugFixed : MonoBehaviour
{
    public GameObject target;
    private void Start()
    {
        GameDB.Instance.xrOriginBugFixedObject = this;

    }
}
