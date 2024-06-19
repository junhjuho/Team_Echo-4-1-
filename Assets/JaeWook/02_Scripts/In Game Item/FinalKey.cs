using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using SeongMin;
using Unity.VisualScripting;
using UnityEngine;


namespace Jaewook
{
    /// <summary>
    /// 3����, Socket �������� Door�� ���� ��� Door�� ������ �ִϸ��̼Ǳ���
    /// ���δ� manager��ũ��Ʈ���� ���� ��ġ ���� ���� -> ItemObject.cs X
    /// </summary>
    public class FinalKey : MonoBehaviour, IItem
    {
        [Header("��ƼŬ �Է�")]
        public new ParticleSystem particleSystem;

        private void Start()
        {
            if (GameManager.Instance.roundManager.round == RoundManager.Round.Three)
            {
                // ��ġ ( �ۼ�Ʈ �ϼ� �� ��ġ )

            }
            this.particleSystem = this.GetComponentInChildren<ParticleSystem>();

        }

        public void OnGrab()
        {
            // Particle�� Child ������Ʈ�� �� ������ ����
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
