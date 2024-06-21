using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static NHR.App;

namespace SeongMin
{
    /// <summary>
    /// 타이틀씬 - Do Tween으로 제작 
    /// </summary>
    public class UITitleSceneMenu : MonoBehaviour
    {
        #region 정면 UI 표시
        [Header("게임 제목 4-1")]
        public TMP_Text titleText;
        [Header("게임 부제 Find and Escape")]
        public TMP_Text subTitleText;

        /// <summary>
        /// Button 
        /// </summary>
        [Header("게임스타트 -> 튜토리얼씬으로 전환")]
        public Button startButton;
        // public Button tutorialButton;
        [Header("게임 정보 UI")]
        public Button gameInfoButton;
        [Header("제작자 정보 UI")]
        public Button makersButton;
        #endregion

        #region 우측 UI 표시

        #endregion

        #region 우측 UI 표시

        #endregion

        // public Button characterSettingButton;
        // public Button gameSettingButton;

        // 키 입력은 다른 씬으로 넘김
        //public UIKeyboard keyboard;

        private void Awake()
        {
            UIManager.Instance.titleSceneMenu = this;

            Transform uiTitle = GameObject.Find("UITitle")?.transform;
            Transform uiTitleRight = GameObject.Find("UITitleRight")?.transform;
            Transform uiTitleLeft = GameObject.Find("UITitleLeft")?.transform;

            if (uiTitle != null)
            {
                #region 정면 UI
                this.titleText = transform.Find("TitleText").GetComponent<TMP_Text>();
                subTitleText = transform.Find("SubTitleText").GetComponent<TMP_Text>();

                startButton = uiTitle.Find("StartButton").GetComponent<Button>();
                gameInfoButton = uiTitle.Find("GameInfoButton").GetComponent<Button>();
                makersButton = uiTitle.Find("MakersButton").GetComponent<Button>();
                #endregion



            }
            else
            {
                Debug.LogError("UITitle Canvas를 찾을 수 없습니다. Hierarchy에서 UITitle 오브젝트의 이름을 확인하세요.");
            }

            // 최적화를 위해서 찾지 못했을 때 오류 처리
            if (titleText == null)
            {
                Debug.LogError("TitleText를 찾을 수 없습니다. Hierarchy에서 TitleText 오브젝트의 이름을 확인하세요.");
            }

            if (subTitleText == null)
            {
                Debug.LogError("SubTitleText를 찾을 수 없습니다. Hierarchy에서 SubTitleText 오브젝트의 이름을 확인하세요.");
            }

            if (startButton == null)
            {
                Debug.LogError("StartButton을 찾을 수 없습니다. Hierarchy에서 StartButton 오브젝트의 이름을 확인하세요.");
            }

            if (gameInfoButton == null)
            {
                Debug.LogError("GameInfoButton을 찾을 수 없습니다. Hierarchy에서 StartButton 오브젝트의 이름을 확인하세요.");

            }

            // 게임시작 -> 튜토리얼 씬 전환 버튼 할당, 
            startButton.onClick.AddListener(() =>
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Tutorial));

            /*
            gameInfoButton.onClick.AddListener(() =>
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.GameInfo));

            // 튜토리얼 - 1라운드 합침 ( 게임 시작 -> 튜토리얼씬으로 전환 )
            tutorialButton.onClick.AddListener(() =>
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Tutorial));
            

             폐기
            characterSettingButton.onClick.AddListener(() => 
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.CharacterCustom));
            */

        }

        void Start()
        {
            /* Awake에서 check 한번 했으니까 필요없을듯
            if (titleText != null)
            {
                AnimateTitle();
            }
            */

            #region 정면 UI Animation
            AnimateTitle();
            AnimateSubTitle();
            AnimateStartButton();
            AnimateGameInfoButton();
            AnimateMakersButton();
            #endregion

            
        }

        void AnimateTitle()
        {
            if (titleText != null)
            {
                // Title 텍스트를 서서히 나타나게 하고, 약간의 떨림 효과를 추가
                titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0);
                titleText.DOFade(1, 1).SetEase(Ease.InOutQuad);
                titleText.rectTransform.anchoredPosition = new Vector2(0, 500);
                titleText.rectTransform.DOAnchorPos(Vector2.zero, 3).SetEase(Ease.OutQuad); // 3초 동안 위치 이동
                // duration, strength, vibrato, randomness, snapping, fadeout
                titleText.rectTransform.DOShakePosition(3, new Vector3(3, 3, 10), 10, 90, false, true).SetDelay(2); // 3초 후 5초 동안 떨림
            }
        }

        void AnimateSubTitle()
        {
            if (subTitleText != null)
            {
                // SubTitle 텍스트를 서서히 나타나게 하고, 약간의 떨림 효과를 추가
                subTitleText.color = new Color(subTitleText.color.r, subTitleText.color.g, subTitleText.color.b, 0);
                subTitleText.DOFade(1, 2).SetEase(Ease.InOutQuad); // 3초 동안 투명도 변화
                subTitleText.rectTransform.anchoredPosition = new Vector2(-500, 0);
                subTitleText.rectTransform.DOAnchorPos(Vector2.zero, 3).SetEase(Ease.OutQuad); // 3초 동안 위치 이동
                subTitleText.rectTransform.DOShakePosition(3, new Vector3(3, 3, 10), 10, 90, false, true).SetDelay(2);
            }
        }

        void AnimateStartButton()
        {
            if (startButton != null)
            {
                // Start 버튼을 서서히 나타나게 하고, 약간의 떨림 효과를 추가
                CanvasGroup canvasGroup = startButton.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = startButton.gameObject.AddComponent<CanvasGroup>();
                }
                canvasGroup.alpha = 0;
                canvasGroup.DOFade(1, 2).SetEase(Ease.InOutQuad).SetDelay(1);
                startButton.transform.DOShakePosition(1, new Vector3(5, 5, 0), 10, 90, false, true).SetDelay(2);
            }
        }

        void AnimateGameInfoButton()
        {
            if (gameInfoButton != null)
            {
                // GameInfo 버튼을 서서히 나타나게 하고, 약간의 떨림 효과를 추가
                CanvasGroup canvasGroup = gameInfoButton.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = gameInfoButton.gameObject.AddComponent<CanvasGroup>();
                }
                canvasGroup.alpha = 0;
                canvasGroup.DOFade(1, 2).SetEase(Ease.InOutQuad).SetDelay(2); // 2초 지연 후 3초 동안 투명도 변화
                gameInfoButton.transform.DOShakePosition(1, new Vector3(5, 5, 0), 10, 90, false, true).SetDelay(1);
            }
        }

        void AnimateMakersButton()
        {
            if(makersButton != null)
            {
                // MakersButton 버튼을 서서히 나타나게 하고, 약간의 떨림 효과를 추가
                CanvasGroup canvasGroup = gameInfoButton.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = gameInfoButton.gameObject.AddComponent<CanvasGroup>();
                }
                canvasGroup.alpha = 0;
                canvasGroup.DOFade(1, 2).SetEase(Ease.InOutQuad).SetDelay(2); // 2초 지연 후 3초 동안 투명도 변화
                gameInfoButton.transform.DOShakePosition(1, new Vector3(5, 5, 0), 10, 90, false, true).SetDelay(1);

            }
        }
    }

}
