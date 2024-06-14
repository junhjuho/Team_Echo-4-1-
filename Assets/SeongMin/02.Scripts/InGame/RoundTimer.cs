using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeongMin
{
    public class RoundTimer : MonoBehaviour
    {
        [Header("라운드 타이머 시간 조절")]
        public int timer = 60;

        WaitForSecondsRealtime waitOneSecond = new WaitForSecondsRealtime(1f);
        private void Awake()
        {
            GameManager.Instance.roundTimer = this;
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
                print("타이머가 돌고 있습니다.");
                yield return waitOneSecond;
                _timer--;
                UIManager.Instance.inGameSceneMenu.timer.text = _timer.ToString();
            }
            GameManager.Instance.inGameSceneManager.Lose();
            yield break;
        }
    }

}