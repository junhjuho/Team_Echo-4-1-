using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    public TMP_Text txtGameResult;

    public void IsWin(bool isWin)
    {
        string role;
        if (GameDB.Instance.playerMission.isChaser) role = "������";
        else role = "������";

        string dialog;

        if (isWin) dialog = string.Format("{0} �¸�", role);
        else dialog = string.Format("{0} �й�", role);

        StartCoroutine(this.CTypingDialog(dialog));
    }

    IEnumerator CTypingDialog(string dialog)
    {
        this.txtGameResult.text = "";
        foreach (var c in dialog)
        {
            this.txtGameResult.text += c;
            yield return new WaitForSeconds(0.2f);
        }
    }

}
