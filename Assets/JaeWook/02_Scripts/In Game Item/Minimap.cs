using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using SeongMin;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    Vector3 playpos;
    Image playerImage;

    private void Start()
    {
    }

    private void Update()
    {
        this.playpos = GameDB.Instance.myPlayer.transform.position;

        var playerimagePos = this.playerImage.gameObject.transform.position;
        playerimagePos = new Vector3(playpos.x, playerimagePos.y, playpos.z);

        //
        
    }
}
