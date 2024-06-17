using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class InfoManager 
{
    public static readonly InfoManager Instance = new InfoManager();

    //���� ���

    public string playerPath = string.Format("{0}/playerInfo.json", Application.persistentDataPath);
    public string heightPath = string.Format("{0}/heightInfo.json", Application.persistentDataPath);

    public PlayerInfo PlayerInfo { get; set; }
    public HeightInfo HeightInfo { get; set; }

    private InfoManager() { }

    //���� �Ǻ�
    public bool IsNewbie()
    {
        bool existFile = File.Exists(this.playerPath);
        Debug.LogFormat("exist: {0}", existFile);
        return !existFile;
    }

    //�ʱ�ȭ
    //�÷��̾� ����
    public void PlayerInfoInit()
    {
        Debug.Log("<color=white>PlyerInfo Init</color>");
        this.PlayerInfo = new PlayerInfo();
        this.PlayerInfo.nowCharacterId = 0;
        this.PlayerInfo.nowClothesColorIndex = 0;
        this.PlayerInfo.nowClothesColorName = "Red";
        this.SavePlayerInfo();
    }
    //Ű ����
    public void HeightInfoInit()
    {
        Debug.Log("<color=white>heightInfo Init</color>");
        this.HeightInfo = new HeightInfo();
        this.HeightInfo.height = 0;
        this.SaveHeightInfo();
    }

    //���� �ε�
    //�÷��̾�
    public void LoadPlayerInfo()
    {
        //������ȭ
        var json = File.ReadAllText(this.playerPath);
        this.PlayerInfo = JsonConvert.DeserializeObject<PlayerInfo>(json);
        Debug.Log("<color=red>[load success] playerInfo.json</color>");
    }
    //Ű
    public void LoadHeightInfo()
    {
        //������ȭ
        var json = File.ReadAllText(this.heightPath);
        this.HeightInfo = JsonConvert.DeserializeObject<HeightInfo>(json);
        Debug.Log("<color=red>[load success] heightInfo.json</color>");
    }

    //���� ����
    //�÷��̾�
    public void SavePlayerInfo()
    {
        //����ȭ
        var json = JsonConvert.SerializeObject(this.PlayerInfo);
        File.WriteAllText(playerPath, json);
        Debug.Log("<color=red>[save success] playerInfo.json</color>");
    }
    //Ű
    public void SaveHeightInfo()
    {
        //����ȭ
        var json = JsonConvert.SerializeObject(HeightInfo);
        File.WriteAllText(heightPath, json);
        Debug.Log("<color=red>[save success] heightInfo.json</color>");
    }

    //���� �߰�/���� �ʿ� ��


    //���� ��������(�迭�� �� ���) �ʿ� ��
    public void EditPlayerInfo(int characterId, int colorIndex, string colorName)
    {
        this.PlayerInfo.nowCharacterId = characterId;
        this.PlayerInfo.nowClothesColorIndex= colorIndex;
        this.PlayerInfo.nowClothesColorName = colorName;
        SavePlayerInfo();
    }
}
