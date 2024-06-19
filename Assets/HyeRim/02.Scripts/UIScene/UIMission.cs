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
                Debug.LogFormat("<color=yellow>�̼� ���� �̺�Ʈ</color>");

                //�̼� ������Ʈ �迭 �޾ƿ���
                var playerMissionObjects = GameDB.Instance.playerMission.playerMissionArray;
                for (int i = 0; i < playerMissionObjects.Length; i++)
                {
                    var mission = Instantiate(Resources.Load<Mission>("UI/Mission/Mission"), this.transform);
                    mission.targetItem = playerMissionObjects[i].GetComponent<ItemObject>();
                    mission.textFirstStep.text = playerMissionObjects[i].name + "ȹ���ϱ�";
                    this.missions.Add(mission);
                }
                //��Ȱ��ȭ
                Invoke("CloseUI", 2f);
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
