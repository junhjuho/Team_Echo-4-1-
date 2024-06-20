using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace NHR
{
    public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
    {

        [Header("캐릭터 커스텀")]
        public Character[] characters;
        public Character nowCharacter;

        public SmartWatchCustomInteractable watch;

        public int nowCharacterID;
        public static PlayerController localPlayer;
        private void Awake()
        {
            GameDB.Instance.playerController = this;

            if (this.watch == null) this.watch = GetComponentInChildren<SmartWatchCustomInteractable>();

            this.nowCharacterID = InfoManager.Instance.PlayerInfo.nowCharacterId;
        }
        private void Start()
        {
            if(photonView.IsMine)
            {
                localPlayer = this;
                Debug.LogFormat("<color=green>PlayerID : {0}</color>", InfoManager.Instance.PlayerInfo.nowCharacterId);
            }
            this.nowCharacterID = InfoManager.Instance.PlayerInfo.nowCharacterId;

            //foreach (var character in this.characters)
            //{
            //    character.gameObject.SetActive(false);
            //}
            this.characters[nowCharacterID].gameObject.SetActive(true);
            this.nowCharacter = this.characters[nowCharacterID];
            SeongMin.GameDB.Instance.playerMission.currentRunnerCharacrer = this.characters[nowCharacterID];
        }
        public void UpdateCharacter(int id)
        {
            Debug.Log("UpdateCharacter");
            photonView.RPC("UpdateCharacterRPC", RpcTarget.All, id);
        }

        [PunRPC]
        public void UpdateCharacterRPC(int id)
        {
            Debug.Log("UpdateCharacterRPC");

            //if (!photonView.IsMine) return;
            this.nowCharacterID = id;

            //foreach (var character in this.characters)
            //{
            //    character.gameObject.SetActive(false);
            //}

            this.ApplyCharacter(id);
        }

        public void ApplyCharacter(int id)
        {
            if(this.nowCharacter != null)
            {
                this.nowCharacter.gameObject.SetActive(false);
            }
            this.nowCharacter = this.characters[id];
            this.nowCharacterID = id;

            if (this.nowCharacter != null)
            {
                this.nowCharacter.gameObject.SetActive(true);
            }
            SeongMin.GameDB.Instance.playerMission.currentRunnerCharacrer = this.characters[id];
        }

        //private void Update()
        //{
        //    if (!photonView.IsMine) return;
        //    this.nowCharacterID = InfoManager.Instance.PlayerInfo.nowCharacterId;

        //    //foreach (var character in this.characters)
        //    //{
        //    //    character.gameObject.SetActive(false);
        //    //}
        //    this.characters[nowCharacterID].gameObject.SetActive(true);
        //    Debug.Log(this.nowCharacterID);
        //}
        //private IEnumerator Start()
        //{
        //    // 나의 클라이언트가 네트워크에 연결될때까지 기달리기
        //    yield return new WaitUntil(() => PhotonNetwork.IsConnected);

        //    //캐릭터 커스텀 설정
        //    var id = 0;
        //    this.photonView.RPC("ApplyCustom", RpcTarget.AllBufferedViaServer, id);
        //}

        //[PunRPC]
        //public void ApplyCustom(int characterID)
        //{
        //    //내 것이 아니면 return
        //    if (!photonView.IsMine) return;
        //    if (this.photonView.IsMine)
        //    {
        //        //int id = InfoManager.Instance.PlayerInfo.nowCharacterId;
        //        //string colorName = InfoManager.Instance.PlayerInfo.nowClothesColorName;
        //        Debug.LogFormat("<color=yellow>Set id {0}</color>", characterID);
        //        foreach (var character in this.characters)
        //        {
        //            character.gameObject.SetActive(false);
        //        }

        //        this.characters[characterID].gameObject.SetActive(true);
        //        SeongMin.GameDB.Instance.playerMission.currentRunnerCharacrer = this.characters[characterID];
        //        //var mat = this.characters[characterID].material;
        //        //Debug.LogFormat("<color=yellow>character : {0}, texture : {1}</color>", characterID, colorName);
        //        //mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + id + colorName);

        //    }

        //}

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            //if (stream.IsWriting)   //isMine인 경우
            //{
            //    stream.SendNext(this.nowCharacterID);
            //}
            //else this.nowCharacterID = (int)stream.ReceiveNext();
        }
    }
    //public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
    //{

    //    [Header("캐릭터 커스텀")]
    //    public Character[] characters;
    //    public Character nowCharacter;

    //    public SmartWatchCustomInteractable watch;

    //    public int nowCharacterID;
    //    private void Awake()
    //    {
    //        GameDB.Instance.playerController = this;

    //        if (this.watch == null) this.watch = GetComponentInChildren<SmartWatchCustomInteractable>();

    //        this.nowCharacterID = InfoManager.Instance.PlayerInfo.nowCharacterId;
    //    }
    //    private void Start()
    //    {
    //        this.nowCharacterID = InfoManager.Instance.PlayerInfo.nowCharacterId;

    //        //foreach (var character in this.characters)
    //        //{
    //        //    character.gameObject.SetActive(false);
    //        //}
    //        this.characters[nowCharacterID].gameObject.SetActive(true);
    //        this.nowCharacter = this.characters[nowCharacterID];
    //        SeongMin.GameDB.Instance.playerMission.currentRunnerCharacrer = this.characters[nowCharacterID];
    //    }
    //    public void UpdateCharacter(int id)
    //    {
    //        if (!photonView.IsMine) return;
    //        this.nowCharacterID = id;

    //        //foreach (var character in this.characters)
    //        //{
    //        //    character.gameObject.SetActive(false);
    //        //}
    //        this.nowCharacter.gameObject.SetActive(false);
    //        this.characters[nowCharacterID].gameObject.SetActive(true);
    //        this.nowCharacter = this.characters[nowCharacterID];
    //        SeongMin.GameDB.Instance.playerMission.currentRunnerCharacrer = this.characters[nowCharacterID];
    //    }

    //    //private void Update()
    //    //{
    //    //    if (!photonView.IsMine) return;
    //    //    this.nowCharacterID = InfoManager.Instance.PlayerInfo.nowCharacterId;

    //    //    //foreach (var character in this.characters)
    //    //    //{
    //    //    //    character.gameObject.SetActive(false);
    //    //    //}
    //    //    this.characters[nowCharacterID].gameObject.SetActive(true);
    //    //    Debug.Log(this.nowCharacterID);
    //    //}
    //    //private IEnumerator Start()
    //    //{
    //    //    // 나의 클라이언트가 네트워크에 연결될때까지 기달리기
    //    //    yield return new WaitUntil(() => PhotonNetwork.IsConnected);

    //    //    //캐릭터 커스텀 설정
    //    //    var id = 0;
    //    //    this.photonView.RPC("ApplyCustom", RpcTarget.AllBufferedViaServer, id);
    //    //}

    //    //[PunRPC]
    //    //public void ApplyCustom(int characterID)
    //    //{
    //    //    //내 것이 아니면 return
    //    //    if (!photonView.IsMine) return;
    //    //    if (this.photonView.IsMine)
    //    //    {
    //    //        //int id = InfoManager.Instance.PlayerInfo.nowCharacterId;
    //    //        //string colorName = InfoManager.Instance.PlayerInfo.nowClothesColorName;
    //    //        Debug.LogFormat("<color=yellow>Set id {0}</color>", characterID);
    //    //        foreach (var character in this.characters)
    //    //        {
    //    //            character.gameObject.SetActive(false);
    //    //        }

    //    //        this.characters[characterID].gameObject.SetActive(true);
    //    //        SeongMin.GameDB.Instance.playerMission.currentRunnerCharacrer = this.characters[characterID];
    //    //        //var mat = this.characters[characterID].material;
    //    //        //Debug.LogFormat("<color=yellow>character : {0}, texture : {1}</color>", characterID, colorName);
    //    //        //mat.mainTexture = Resources.Load<Texture>("ClothesTexture/" + id + colorName);

    //    //    }

    //    //}

    //    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //    {
    //        if (stream.IsWriting)   //isMine인 경우
    //        {
    //            stream.SendNext(this.nowCharacterID);
    //        }
    //        else this.nowCharacterID = (int)stream.ReceiveNext();
    //    }
    //}

}
