using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XROriginBugFixed : MonoBehaviour
{
    private void Awake()
    {
        GameDB.Instance.xrOriginBugFixedObject = this;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = GameDB.Instance.playerMission.currentRunnerPrefab.transform.position;
    }
}
