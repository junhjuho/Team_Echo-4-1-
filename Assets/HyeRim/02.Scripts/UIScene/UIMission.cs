using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NHR
{
    public class UIMission : MonoBehaviour
    {
        public List<Mission> missions;

        private void Start()
        {
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Get_Mission, new EventHandler((type) =>
            {
                Debug.LogFormat("<color=yellow>미션 배정 이벤트</color>");

                GameObject[] playerMissionObjects;
                if (GameDB.Instance.playerMission.isChaser) playerMissionObjects = GameDB.Instance.playerMission.chaserMissionArray;
                else playerMissionObjects = GameDB.Instance.playerMission.playerMissionArray;
                //var playerMissionObjects = GameDB.Instance.playerMission.playerMissionArray;

                //미션 오브젝트 배열 받아오기
                for (int i = 0; i < playerMissionObjects.Length; i++)
                {
                    var mission = Instantiate(Resources.Load<Mission>("UI/Mission/Mission"), this.transform);
                    mission.targetItem = playerMissionObjects[i].GetComponent<ItemObject>();
                    mission.itemName = mission.targetItem.name;
                    mission.textFirstStep.text = mission.itemName + "획득하기";
                    this.missions.Add(mission);
                }
                //비활성화
                //Invoke("CloseUI", 2f);
            }));
            EventDispatcher.instance.AddEventHandler<string>((int)NHR.EventType.eEventType.Complete_Mission, new EventHandler<string>((type, name) =>
            {
                Debug.Log("Complete_Mission in UIMission");
                for (int i = 0; i < this.missions.Count; i++)
                {
                    if (this.missions[i].itemName == name)
                    {
                        this.missions[i].textFirstStep.fontStyle = FontStyles.Strikethrough;
                        break;
                    }
                }
            }));

        }
        //private void CloseUI()
        //{
        //    this.gameObject.SetActive(false);
        //}
        //public void UpdateMissions()
        //{
        //    foreach (var mission in this.missions) mission.UpdateMission();
        //}
    }
    

}
