using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Search;

namespace Jaewook
{
    public class TitleSceneManager : MonoBehaviour
    {
        private static TitleSceneManager instance;
        public static TitleSceneManager Instance { get { return instance; } }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
        public UITitleLeft uiTitleLeft;
        public UITitleRight uiTitleRight;
        public UIMakers uiMakers;
        public UIGameInfo uiGameInfo;

        public void ShowCanvas(Transform canvasToShow)
        {
            canvasToShow.gameObject.SetActive(true);
        }

        public void HideCanvas(Transform canvasToHide)
        {
            canvasToHide.gameObject.SetActive(false);
        }


        public void SwitchCanvas(Transform canvasToShow, Transform canvasToHide)
        {
            canvasToHide.gameObject.SetActive(false);
            canvasToShow.gameObject.SetActive(true);
        }
    }

}
