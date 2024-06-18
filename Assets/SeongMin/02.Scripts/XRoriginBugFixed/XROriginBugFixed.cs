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
        if (GameDB.Instance.playerMission.currentRunnerPrefab != null)
            target = GameDB.Instance.playerMission.currentRunnerPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.transform.position;
    }
}
