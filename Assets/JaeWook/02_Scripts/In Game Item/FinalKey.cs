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
    /// </summary>
    public class FinalKey : IItem
    {
        private void Start()
        {
            if(GameManager.Instance.roundManager.round == RoundManager.Round.Three)
            {
                // ��ġ ( �ۼ�Ʈ �ϼ� �� ��ġ )

                
            }
            
            
        }
        
        public void OnGrab()
        {
            throw new System.NotImplementedException();
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
