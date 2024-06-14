using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeongMin
{
    public class TutorialSceneManager : MonoBehaviour
    {
        public List<Transform> tutorialObjectPositionList;
        public List<GameObject> tutorialObjectList;
        private void Awake()
        {
            GameManager.Instance.tutorialSceneManager = this;
        }

        private void Start()
        {
            TutorialSetting();
        }

        private void TutorialSetting()
        {
            // Ʃ�丮�� ������Ʈ �ʱ�ȭ 
            // ps. Ʃ�丮�� ������Ʈ�� �ּ��� �Ѱ����� �־�� �ϱ⿡
            // ���� ����Ʈ�� �׻� 
            // Ʃ�丮�� ������Ʈ �������� �����Ӱ� ������ �ְ� �ؾ��Ѵ�.
            // �׸��� Ʃ�丮�� ������Ʈ ������ŭ�� for���� ���ȴ�.
            if (tutorialObjectList.Count <= tutorialObjectPositionList.Count)
            {
                for (int i = 0; i < tutorialObjectList.Count; i++)
                {
                    var tutorialObject = Instantiate(tutorialObjectList[i]);
                    tutorialObjectList[i].transform.position = tutorialObjectPositionList[i].position;
                }
            }
            else
            {
                print("�����ؾ��� ������Ʈ ���� ���� ������ ��ġ���� �����ϴ�. ������ġ�� �߰����ּ���.");
            }
        }
    }

}