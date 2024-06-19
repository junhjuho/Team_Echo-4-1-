using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using SeongMin;
using Unity.VisualScripting;
using UnityEngine;


namespace Jaewook
{
    /// <summary>
    /// 3라운드, Socket 형식으로 Door에 갖다 대면 Door가 열리는 애니메이션까지
    /// 성민님 manager스크립트에서 생성 배치 까지 구현 -> ItemObject.cs X
    /// </summary>
    public class FinalKey : MonoBehaviour, IItem
    {
        [Header("파티클 입력")]
        public new ParticleSystem particleSystem;

        private void Start()
        {
            if (GameManager.Instance.roundManager.round == RoundManager.Round.Three)
            {
                // 배치 ( 퍼센트 완성 시 배치 )

            }
            this.particleSystem = this.GetComponentInChildren<ParticleSystem>();

        }

        public void OnGrab()
        {
            // Particle이 Child 오브젝트로 들어가 있으면 실행
            if(particleSystem != null)
            {
                particleSystem.Play();
            }
        }

        public void OnUse()
        {
            throw new System.NotImplementedException();
        }

        public void OnRelease()
        {
            throw new System.NotImplementedException();
        }
    }

}
