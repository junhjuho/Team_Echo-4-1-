using System.Collections;
using System.Collections.Generic;
using NHR;
using Photon.Realtime;
using SeongMin;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

namespace Jaewook
{
    public class Minimap : MonoBehaviour
    {
        public Camera minimapCamera;
        public RenderTexture minimapTexture;
        public Light directionalLight;
        // public GameObject playerPos;
        // public Minimap minimap;

        //public Material minimapMaterial;
        
        void Start()
        {
            /*
            playerPos = GameDB.Instance.myPlayer;
            */
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
            /*
            this.minimap.playerPos.transform.position = new Vector3(playerPos.transform.position.x,
                this.minimap.playerPos.transform.position.y,
                playerPos.transform.position.z);
            */

            if (directionalLight != null)
            {
                // Directional Light가 켜져 있는 상태를 미니맵에 반영
                if (directionalLight.enabled)
                {
                    // Directional Light가 켜져 있을 때의 설정
                    minimapCamera.backgroundColor = Color.white; // 예시로 배경색을 변경
                }
                else
                {
                    // Directional Light가 꺼져 있을 때의 설정
                    directionalLight.enabled = true;
                }

            }
        }
    }

}
