using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using Newtonsoft.Json;
public class DataManager
{
    public static readonly DataManager Instance = new DataManager();

    private DataManager() { }

    //�����͵�
    private Dictionary<int, CharacterData> dicCharacterData;
    private Dictionary<int, TutorialData> dicTutorialData;
    private Dictionary<string, EventDialogData> dicEventDialogData;

    public int totalTutorialIndex;

    //Json ������ �ε�
    //ĳ���� ������
    public void LoadCharacterData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Datas/characterData");

        var json = asset.text;
        Debug.LogFormat("<color=red>character data load:{0}</color>", json);

        //������ȭ
        CharacterData[] datas = JsonConvert.DeserializeObject<CharacterData[]>(json);

        this.dicCharacterData = datas.ToDictionary(x => x.id);
        Debug.LogFormat("character data loaded : {0}", this.dicCharacterData);
    }
    //Ʃ�丮�� ������
    public void LoadTutorialData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Datas/tutorialData");

        var json = asset.text;
        Debug.LogFormat("<color=red>tutorial data load:{0}</color>", json);

        //������ȭ
        TutorialData[] datas = JsonConvert.DeserializeObject<TutorialData[]>(json);

        this.dicTutorialData = datas.ToDictionary(x => x.id);
        Debug.LogFormat("tutorial data loaded : {0}", this.dicTutorialData);

        this.totalTutorialIndex = dicTutorialData.Count;
    }
    //�̺�Ʈ dialog ������
    public void LoadEventDialogData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Datas/eventDialogData");

        var json = asset.text;
        Debug.LogFormat("<color=red>eventDialog data load:{0}</color>", json);

        //������ȭ
        EventDialogData[] datas = JsonConvert.DeserializeObject<EventDialogData[]>(json);

        this.dicEventDialogData = datas.ToDictionary(x => x.eventType);
        Debug.LogFormat("tutorial data loaded : {0}", this.dicEventDialogData);
    }

    //������ ����
    //ĳ���� ������
    public CharacterData GetCharacterData(int id)
    {
        return this.dicCharacterData[id];
    }
    //Ʃ�丮�� ������
    public TutorialData GetTutorialData(int id)
    {
        return this.dicTutorialData[id + 100];
    }
    //�̺�Ʈ ���̾�α� ������
    public string GetEventDialog(string eventType)
    {
        return this.dicEventDialogData[eventType].dialog;
    }
}
