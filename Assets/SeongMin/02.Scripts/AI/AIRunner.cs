using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SeongMin
{
    public class AIRunner : MonoBehaviour,IDamageable
    {
        private WaitForSecondsRealtime oneSecond = new WaitForSecondsRealtime(1f);
        private WaitUntil nextThink;
        private NavMeshAgent agent;
        private Animator animator;
        private Transform targetPosition;
        private PhotonView photonView;
        private AudioSource audioSource;
        private bool changeState = false;
        private int rand = 0;
        public GameObject bloodEffect;
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
            audioSource = GetComponent<AudioSource>();
        }
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => PhotonNetwork.IsConnected);
            while (state != State.Die)
            {
                
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
                yield return nextThink;
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
            rand = Random.Range(0, GameDB.Instance.escapeDoorPositionList.Count);
            targetPosition = GameDB.Instance.escapeDoorPositionList[rand];
            agent.SetDestination(targetPosition.position);
        }

        public void OnHit(Collider other)
        {
            Debug.Log("OnHit : " + other.gameObject.name);
            if(other.gameObject.name == "Fireaxe")
            {
                agent.speed = 0;
                state = State.Die;
                animator.SetTrigger("isDie");
                Invoke("Die", 0.5f);

                var effectPos = this.transform.position + Vector3.up * 1.27f;
                Instantiate(bloodEffect, effectPos, Quaternion.identity);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log("AI Ãæµ¹ : " + other.gameObject.name);
            OnHit(other);
            //other.bounds.ClosestPoint(other.transform.position);
        }

        public void Die()
        {
            this.gameObject.SetActive(false);
        }
    }

}