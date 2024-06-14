using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public enum SceneValue
    {
        tutorial,
        lobby,
        inGame
    }

    [Header("�� ���� �°� �����ϱ�")]
    public SceneValue sceneValue;
    private void Start()
    {
        // �κ�Ŵ����� ����Ʈ�� �� ������Ʈ �߰� 
        if (sceneValue == SceneValue.lobby)
            GameManager.Instance.lobbySceneManager.playerSpawnPointList.Add(this.transform);
        else if (sceneValue == SceneValue.inGame)
            GameManager.Instance.inGameMapManager.playerSpawnPositionList.Add(this.transform);

    }
}
