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
    /// </summary>
    public class FinalKey : IItem
    {
        private void Start()
        {
            if(GameManager.Instance.roundManager.round == RoundManager.Round.Three)
            {
                // 배치 ( 퍼센트 완성 시 배치 )

                
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
