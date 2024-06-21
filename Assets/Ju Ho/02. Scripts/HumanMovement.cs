using NHR;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;

public class HumanMovement : PlayerMovement, IDamageable
{
    public MonsterMovement monsterMovement;
    public GameObject FireAxe;
    public DieAnimation[] dieAnims;


    public bool isRunBtnDown;
    bool isEnergyDown;
    bool isDie;
    UIPlayer uiPlayer;

    Scene scene;

    PlayerSyncController playerSyncController;

    

    public void OnEnable()
    {
        isDie = false;
    }

    public override void Start()
    {
        base.Start();
        scene = SceneManager.GetActiveScene();
        playerSyncController = this.GetComponentInParent<PlayerSyncController>();
    }

    void OnDisable() // 
    {
        if (isDie && pv.IsMine)
        {
            //Vector3 zombiePos = monsterMovement.transform.position - this.transform.position;
            //zombiePos.Normalize();
            //float attackPos = Vector3.Dot(this.transform.forward, zombiePos);

            //string dirDie = attackPos > 0 ? "Backward Die" : "Forward Die";

            for(int i = 0; i < dieAnims.Length; i++)
            {
                if(this.gameObject.name + " Die Model" == dieAnims[i].gameObject.name) // 현재 오브젝트의 이름과 모델 애니메이션 오브젝트 이름이 같으면
                {
                    dieAnims[i].transform.gameObject.SetActive(true); // 모델 애니메이션 오브젝트를 활성화 시키고 애니메이션 실행
                    dieAnims[i].PlayerDieAnimation("Backward Die");
                    break;
                }
            }

            var heart = SeongMin.GameManager.Instance.playerManager.heart;
            EventDispatcher.instance.SendEvent<int>((int)NHR.EventType.eEventType.Notice_Attacked, heart);
            heart--;

            isDie = false;
        }
    }

    void Update()
    {
        PlayerMove();
        FingerMove(animator);
    }
    public override void PlayerMove() // 플레이어 걷기 , 달리기
    {
        if (pv.IsMine)
        {
            base.PlayerMove();  // PlayerMovement 스크립트 상속

            if (scene.name == ("InGameScene 1"))
            {
                SeongMin.GameManager.Instance.playerManager.humanMovement = this;
                isEnergyDown = SeongMin.GameManager.Instance.playerManager.uiPlayer.isEnergyDown;
            }
            else // InGameScene 1이 아닐 때, 
            {
                isEnergyDown = false;
            }

            isRunBtnDown = inputActionAsset.actionMaps[4].actions[11].IsPressed(); // 달리기 버튼

            float moveBlendtree = isRunBtnDown && !isEnergyDown ? 1f : 0.5f; // 달리기 버튼에 따른 블렌드 트리

            moveProvider.moveSpeed = isRunBtnDown && !isEnergyDown ? 4f : 2f; // 달리기 버튼에 따른 속도
            
            animator.SetFloat("Move", dir.magnitude * moveBlendtree);
        }
        else
            return;
    }

    public void FingerMove(Animator animator) // 손가락 애니메이션
    {
        if (pv.IsMine)
        {
            float leftTriggerValue = inputActionAsset.actionMaps[2].actions[3].ReadValue<float>();
            animator.SetFloat("Left Trigger", leftTriggerValue);

            float leftGripValue = inputActionAsset.actionMaps[2].actions[1].ReadValue<float>();
            animator.SetFloat("Left Grip", leftGripValue);

            float rightTriggerValue = inputActionAsset.actionMaps[5].actions[3].ReadValue<float>();
            animator.SetFloat("Right Trigger", rightTriggerValue);

            float rightGripValue = inputActionAsset.actionMaps[5].actions[1].ReadValue<float>();
            animator.SetFloat("Right Grip", rightGripValue);
        }
        else
            return;
    }

    public void OnTriggerEnter(Collider other)
    {
        OnHit(other); // 충돌했을 때 OnHit 실행
    }

    void RespawnPlayer()
    {
        playerSyncController.origin.transform.position =
            SeongMin.GameManager.Instance.inGameMapManager.playerSpawnPositionList[0].position;
    }

    public void OnHit(Collider other) // 때린 물체가 fireaxe라면 오브젝트 비활성화, OnDisable실행
    {
        if (pv.IsMine && other.gameObject.name == "fireaxe")
        {
            isDie = true;
            this.gameObject.SetActive(false);
        }
    }
}
