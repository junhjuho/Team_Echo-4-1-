using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static SeongMin.ItemObject;

public class TriggerCheck : MonoBehaviour
{
    ItemObject itemObject;
    void Start()
    {
        if (this.gameObject.TryGetComponent(out ItemObject item))
        {
            itemObject = this.gameObject.GetComponent<ItemObject>();
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("This Name : " + this.name + "itemObject : " + itemObject);
        if (other.gameObject.TryGetComponent(out PlayerMovement player) && player.pv.IsMine)
        {
            var canvas = GameDB.Instance.itemInfomationCanvas;
            canvas.transform.position = this.transform.position + (Vector3.up * 2f);
            canvas.gameObject.transform.LookAt(player.transform.position);
            canvas.image.SetActive(true);
            canvas.text.gameObject.SetActive(true);
            canvas.text.text = this.gameObject.name;
        }
        if (itemObject != null && itemObject.charactorValue == CharactorValue.chaser)
        {
            itemObject.fx.SetActive(true);
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
