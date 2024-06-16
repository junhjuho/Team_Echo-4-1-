using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SeongMin
{
    public class AIRunner : MonoBehaviour
    {
        private WaitForSecondsRealtime oneSecond = new WaitForSecondsRealtime(1f);
        private NavMeshAgent agent;
        private Animator animator;
        private Transform targetPosition;

        public enum State
        {
            Die,
            Idle,
            Move
        }
        State state = State.Idle;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }
        private IEnumerator Start()
        {
            while (state != State.Die)
            {
                yield return oneSecond;
            }
            yield break;
        }
    }

}