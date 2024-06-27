using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeongMin
{
    public class RoundTimer : MonoBehaviour
    {
        [Header("���� Ÿ�̸� �ð� ����")]
        public int timer = 190;
        [Header("���� ���� Ÿ�̸� �ð� ����")]
        public float monsterTimer = 10;

        WaitForSecondsRealtime waitOneSecond = new WaitForSecondsRealtime(1f);
        private void Awake()
        {
            GameManager.Instance.roundTimer = this;
        }
        private void Start()
        {
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Change_Monster, new EventHandler((type) =>
            {
                this.MonsterTimerStart();
            }));

        }
        public void TimerStart()
        {
            StartCoroutine(Timer());
        }
        private IEnumerator Timer()
        {
            int _timer = timer;
            while (_timer>0)
            {
                print("Ÿ�̸Ӱ� ���� �ֽ��ϴ�.");
                yield return waitOneSecond;
                _timer--;
                //UIManager.Instance.inGameSceneMenu.timer.text = _timer.ToString();
                EventDispatcher.instance.SendEvent<int>((int)NHR.EventType.eEventType.Update_Timer, _timer);
            }
            //GameManager.Instance.inGameSceneManager.Lose();
            GameDB.Instance.isWin = false;
            EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Notice_Result);
            StartCoroutine(EndCoroutine());
            yield break;
        }
        public void MonsterTimerStart()
        {
            StartCoroutine(this.MonsterTimer());
        }
        private IEnumerator MonsterTimer()
        {
            float _monsterTimer = this.monsterTimer;
            while (_monsterTimer > 0)
            {
                Debug.Log("���� ���� ��");
                yield return waitOneSecond;
                _monsterTimer--;
                EventDispatcher.instance.SendEvent<float>((int)NHR.EventType.eEventType.Update_MonsterTimer, _monsterTimer);
            }
            GameDB.Instance.playerMission.photonView.RPC("CharacterChange",Photon.Pun.RpcTarget.All, "Player");
            //���� ���� Ǯ��
            EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_EventUI, "chaserChangeOff");
            yield break;
        }

        IEnumerator EndCoroutine()
        {
            yield return new WaitForSecondsRealtime(2f);
            GameDB.Instance.playerMission.WinCheck("ChaserWin");
        }
    }

}