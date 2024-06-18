using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XROriginBugFixed : MonoBehaviour
{
    public GameObject target;
    private void Awake()
    {
        GameDB.Instance.xrOriginBugFixedObject = this;
    }
    private void Start()
    {
        if (GameDB.Instance.playerMission.currentRunnerPrefab != null)
            target = GameDB.Instance.playerMission.currentRunnerPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.transform.position;
    }
}
