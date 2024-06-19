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
        Debug.Log("�̼� �Ϸ�");
        this.textCompleteMission.text = string.Format("{0} ȹ�� �̼� �Ϸ�!", missionName);
        this.textCompleteMission.color = Color.gray;

        Invoke("CloseUI", 0.5f);
    }
    public void CloseUI()
    {
        this.gameObject.SetActive(false);
    }

}
