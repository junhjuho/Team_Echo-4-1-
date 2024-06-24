using SeongMin;
using System.Collections;
using System.Collections.Generic;
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

                //미션 오브젝트 배열 받아오기
                var playerMissionObjects = GameDB.Instance.playerMission.playerMissionArray;
                for (int i = 0; i < playerMissionObjects.Length; i++)
                {
                    var mission = Instantiate(Resources.Load<Mission>("UI/Mission/Mission"), this.transform);
                    mission.targetItem = playerMissionObjects[i].GetComponent<ItemObject>();
                    mission.textFirstStep.text = playerMissionObjects[i].name + "획득하기";
                    this.missions.Add(mission);
                }
                //비활성화
                //Invoke("CloseUI", 2f);
            }));
        }
        private void CloseUI()
        {
            this.gameObject.SetActive(false);
        }
        public void UpdateMissions()
        {
            foreach (var mission in this.missions) mission.UpdateMission();
        }
    }
    

}
