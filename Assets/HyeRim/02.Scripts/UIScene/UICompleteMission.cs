using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICompleteMission : MonoBehaviour
{
    public TMP_Text textCompleteMission;

    private void Awake()
    {
        this.textCompleteMission = GetComponentInChildren<TMP_Text>();
    }
    public void CompleteMission(string missionName)
    {
        Debug.Log("¹Ì¼Ç ¿Ï·á");
        this.textCompleteMission.text = string.Format("{0} È¹µæ ¹Ì¼Ç ¿Ï·á!", missionName);
        this.textCompleteMission.color = Color.gray;

        Invoke("CloseUI", 0.5f);
    }
    public void CloseUI()
    {
        this.gameObject.SetActive(false);
    }

}
