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

        
        
    }

}
