using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using SeongMin;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public Camera minimapCamera;
    public RenderTexture minimapTexture;
    public Light directionalLight;
    //public Material minimapMaterial;

    void Start()
    {
        if (minimapCamera == null)
        {
            Debug.LogError("Minimap Camera is not assigned.");
            return;
        }

        if (minimapTexture == null)
        {
            Debug.LogError("Minimap Render Texture is not assigned.");
            return;
        }

        minimapCamera.targetTexture = minimapTexture;
        //minimapMaterial.mainTexture = minimapTexture;
    }

    void Update()
    {
        if (directionalLight != null)
        {
            // Directional Light�� ���� �ִ� ���¸� �̴ϸʿ� �ݿ�
            if (directionalLight.enabled)
            {
                // Directional Light�� ���� ���� ���� ����
                minimapCamera.backgroundColor = Color.white; // ���÷� ������ ����
                minimapCamera.clearFlags = CameraClearFlags.Skybox;
            }
            else
            {
                // Directional Light�� ���� ���� ���� ����
                directionalLight.enabled = true;
            }
            
        }
    }
}
