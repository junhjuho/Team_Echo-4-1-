using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SeongMin
{
    public class AIRunner : MonoBehaviour
    {
        private WaitForSecondsRealtime oneSecond = new WaitForSecondsRealtime(1f);
        private WaitUntil nextThink;
        private NavMeshAgent agent;
        private Animator animator;
        private Transform targetPosition;
        private bool changeState = false;
        private int rand = 0;
        public enum State
        {
            Die,
            Idle,
            Move
        }
        State state = State.Idle;
        private void Awake()
        {
            nextThink = new WaitUntil(() => changeState);
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }
        private IEnumerator Start()
        {
            while (state != State.Die)
            {
                switch (state)
                {
                    case State.Idle:
                        break;
                    case State.Move:
                        NextTargetSetting();
                        StartCoroutine(MoveCheck());
                        break;
                }
                yield return nextThink;
                changeState = false;
            }
            yield break;
        }
        private void NextTargetSetting()
        {
            rand = Random.Range(0,GameManager.Instance.inGameMapManager.inGameItemPositionList.Count);
            targetPosition = GameManager.Instance.inGameMapManager.inGameItemPositionList[rand];
            agent.SetDestination(targetPosition.position);
        }
        private IEnumerator MoveCheck()
        {
            if(Vector3.Distance(this.transform.position, targetPosition.position) < 3f)
            {
                changeState = true;
                yield break;
            }
            yield return oneSecond;
        }
    }

}