using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SeongMin
{
    public class PlayerAction : XRGrabInteractable
    {
        private void Start()
        {
            this.activated.AddListener(ActivedEvent);
        }
        // �÷��̾ �������� ����� ��,
        private void ActivedEvent(ActivateEventArgs args)
        {
            if (args.interactableObject.transform.TryGetComponent(out ItemObject _item))
            {
                // �÷��̾��� �̼� �迭�� _item ������Ʈ�� ��ġ�ϴ� �� ������ if�� �ȿ� �ڵ带 ����
                if(GameDB.Instance.playerMission.MissionCheck(_item.gameObject, GameDB.Instance.playerMission.playerMissionArray))
                {
                    //TODO ��߼� �������̸�, PunRPC�� ���� �ý��ۿ� ���� ��ġ�� 
                }
            }
        }
    }

}