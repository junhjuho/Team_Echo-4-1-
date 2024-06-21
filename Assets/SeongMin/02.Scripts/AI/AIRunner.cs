using Photon.Pun;
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
        private PhotonView photonView;
        private bool changeState = false;
        private int rand = 0;
        public enum State
        {
            Die,
            Idle,
            Move
        }
        public State state = State.Idle;
        private void Awake()
        {
            nextThink = new WaitUntil(() => changeState);
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            photonView = GetComponent<PhotonView>();
        }
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => PhotonNetwork.IsConnected);
            while (state != State.Die)
            {
                yield return nextThink;
                switch (state)
                {
                    case State.Idle:
                        StartCoroutine(Idle());
                        break;
                    case State.Move:
                        NextTargetSetting();
                        StartCoroutine(Move());
                        break;
                }
                changeState = false;
            }
            yield break;
        }
        private IEnumerator Idle()
        {
            animator.SetFloat("isSpeed", 0);
            while(state == State.Idle)
            {
                yield return oneSecond;
                rand = Random.Range(0, 3);
                if (rand != 0)
                {
                    animator.SetBool("isSit", false);
                    state = State.Move;
                    changeState = true;
                    yield break;
                }
                else
                    animator.SetBool("isSit", true);
            }
        }
        private IEnumerator Move()
        {
            animator.SetFloat("isSpeed", 1f);
            while (state == State.Move)
            {
                if (Vector3.Distance(this.transform.position, targetPosition.position) < 5f)
                {
                    state = State.Idle;
                    changeState = true;
                    yield break;
                }
                yield return oneSecond;
            }
        }
        private void NextTargetSetting()
        {
            rand = Random.Range(0, GameManager.Instance.inGameMapManager.inGameItemPositionList.Count);
            targetPosition = GameManager.Instance.inGameMapManager.inGameItemPositionList[rand];
            agent.SetDestination(targetPosition.position);
        }
    }

}