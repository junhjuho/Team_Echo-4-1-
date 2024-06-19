using System.Collections;
using System.Collections.Generic;
using Jaewook;
using SeongMin;
using UnityEngine;

namespace Jaewook
{
    public class KeyItem : ItemObject, IItem
    {
        // private MissionManager missionManager;

        private void Start()
        {
            base.Start();
            // missionManager = GameManager.Instance.missionManager;
        }

        public void OnGrab()
        {
            /*
            if (missionManager != null)
            {
                missionManager.runnerMissionCount++;
                missionManager.MissionSetting();
                this.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("MissionManager is not assigned.");
            }
            */
            
        }

        public void OnRelease()
        {
            throw new System.NotImplementedException();
        }

        public void OnUse()
        {
            throw new System.NotImplementedException();
        }
    }

}
