using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static SeongMin.ItemObject;

public class TriggerCheck : MonoBehaviour
{
    [Header("자기 ItemObject오브젝트 할당하기")]
    public ItemObject itemObject;
    void Start()
    {
        if (this.gameObject.TryGetComponent(out ItemObject item))
        {
            itemObject = this.gameObject.GetComponent<ItemObject>();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(itemObject != null)
        {
            if (other.gameObject.TryGetComponent(out PlayerMovement player) && player.pv.IsMine)
            {
                var canvas = GameDB.Instance.itemInfomationCanvas;
                canvas.transform.position = this.transform.position + (Vector3.up * 0.8f);
                canvas.gameObject.transform.LookAt(player.transform.position);
                canvas.image.SetActive(true);
                canvas.text.gameObject.SetActive(true);
                canvas.text.text = this.gameObject.name;
                canvas.text.color = Color.white;

                if (GameDB.Instance.playerMission.isChaser == false)
                {
                    // 내 미션 아이템인지 확인
                    if (GameDB.Instance.playerMission.MissionItemCheck(itemObject.gameObject, GameDB.Instance.playerMission.playerMissionArray))
                    {
                        canvas.text.color = Color.green;
                    }
                }
                else
                {
                    // 내 미션 아이템인지 확인
                    if (GameDB.Instance.playerMission.MissionItemCheck(itemObject.gameObject, GameDB.Instance.playerMission.chaserMissionArray))
                    {
                        canvas.text.color = Color.green;
                    }
                }
            }
            if (itemObject.charactorValue == CharactorValue.chaser)
            {
                itemObject.fx.SetActive(true);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement player) && player.pv.IsMine)
        {
            var canvas = GameDB.Instance.itemInfomationCanvas;
            canvas.image.SetActive(false);
            canvas.text.gameObject.SetActive(false);
        }
        if (itemObject != null && itemObject.charactorValue == CharactorValue.chaser)
        {
            itemObject.fx.SetActive(false);
        }
    }

}
