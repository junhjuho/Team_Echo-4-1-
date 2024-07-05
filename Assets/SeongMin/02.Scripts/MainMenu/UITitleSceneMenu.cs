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
    /// 타이틀씬
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
        [Header("게임 정보 UI")]
        public Button gameInfoButton;
        [Header("제작자 정보 UI")]
        public Button makersButton;
        // public Button tutorialButton;
        #endregion

        /*
        #region 우측 UI 표시
        [Header("우측캔버스 메인 타이틀")]
        public TMP_Text titleTextRight;
        [Header("-")]
        public TMP_Text subTitleTextRight;
        #endregion

        #region 좌측 UI 표시 -> 설정 
        [Header("좌측캔버스 메인 타이틀")]
        public TMP_Text titleTextLeft;
        [Header("-")]
        public TMP_Text subTitleTextLeft;
        #endregion
        */

        // public Canvas makersCanvas; // 제작자 정보 UI 캔버스
        // public Image makersImage;
        public Button makersBackButton;

        // public Canvas gameInfoCanvas; // 게임 정보 UI 캔버스
        // public Image gameInfoImage;
        public Button gameInfoBackButton;

        // 키 입력은 다른 씬으로 넘김
        //public UIKeyboard keyboard;

        public void Awake()
        {
            UIManager.Instance.titleSceneMenu = this;

            Transform uiTitle = GameObject.Find("UITitle")?.transform;
            //Transform uiTitleRight = GameObject.Find("UITitleRight")?.transform;
            //Transform uiTitleLeft = GameObject.Find("UITitleLeft")?.transform;

            // Transform makersCanvas = GameObject.Find("MakersCanvas")?.transform;
            // Transform gameInfoCanvas = GameObject.Find("GameInfoCanvas")?.transform;

            
            #region 정면 UI
            if (uiTitle == null)
            {
                Debug.LogError("UITitle Canvas를 찾을 수 없습니다. Hierarchy에서 UITitle 오브젝트의 이름을 확인하세요.");
            }
            else
            {
                titleText = uiTitle.Find("TitleText")?.GetComponent<TMP_Text>();
                subTitleText = uiTitle.Find("SubTitleText")?.GetComponent<TMP_Text>();

                startButton = uiTitle.Find("StartButton")?.GetComponent<Button>();
                gameInfoButton = uiTitle.Find("GameInfoButton")?.GetComponent<Button>();
                makersButton = uiTitle.Find("MakersButton")?.GetComponent<Button>();

                if (titleText == null)
                    Debug.LogError("TitleText를 찾을 수 없습니다. Hierarchy에서 TitleText 오브젝트의 이름을 확인하세요.");
                if (subTitleText == null)
                    Debug.LogError("SubTitleText를 찾을 수 없습니다. Hierarchy에서 SubTitleText 오브젝트의 이름을 확인하세요.");
                if (startButton == null)
                    Debug.LogError("StartButton을 찾을 수 없습니다. Hierarchy에서 StartButton 오브젝트의 이름을 확인하세요.");
                if (gameInfoButton == null)
                    Debug.LogError("GameInfoButton을 찾을 수 없습니다. Hierarchy에서 StartButton 오브젝트의 이름을 확인하세요.");
                if (makersButton == null)
                    Debug.LogError("MakersButton을 찾을 수 없습니다. Hierarchy에서 StartButton 오브젝트의 이름을 확인하세요.");
            }
                #endregion
            /*
            #region 우측 UI
            if (uiTitleRight == null)
            {
                Debug.LogError("UITitleRight Canvas를 찾을 수 없습니다. Hierarchy에서 UITitleRight 오브젝트의 이름을 확인하세요.");
            }
            else
            {
                titleTextRight = uiTitleRight.Find("TitleTextRight")?.GetComponent<TMP_Text>();
                subTitleTextRight = uiTitleRight.Find("SubTitleTextRight")?.GetComponent<TMP_Text>();

                if (titleTextRight == null)
                    Debug.LogError("TitleTextRight를 찾을 수 없습니다. Hierarchy에서 TitleTextRight 오브젝트의 이름을 확인하세요.");
                if (subTitleTextRight == null)
                    Debug.LogError("SubTitleTextRight를 찾을 수 없습니다. Hierarchy에서 SubTitleTextRight 오브젝트의 이름을 확인하세요.");
            }
                #endregion
            
            #region 좌측 UI
            if (uiTitleLeft == null)
            {
                Debug.LogError("UITitleLeft Canvas를 찾을 수 없습니다. Hierarchy에서 UITitleLeft 오브젝트의 이름을 확인하세요.");
            }
            else
            {
                titleTextLeft = uiTitleLeft.Find("TitleTextLeft")?.GetComponent<TMP_Text>();
                subTitleTextLeft = uiTitleLeft.Find("SubTitleTextLeft")?.GetComponent<TMP_Text>();

                if (titleTextLeft == null)
                    Debug.LogError("TitleTextLeft를 찾을 수 없습니다. Hierarchy에서 TitleTextLeft 오브젝트의 이름을 확인하세요.");
                if (subTitleTextLeft == null)
                    Debug.LogError("SubTitleTextLeft를 찾을 수 없습니다. Hierarchy에서 SubTitleTextLeft 오브젝트의 이름을 확인하세요.");
            }
            #endregion
            */

            /*
            #region Makers Ui
            if (makersCanvas == null)
            {
                Debug.LogError("MakersCanvas를 찾을 수 없습니다. Hierarchy에서 MakersCanvas 오브젝트의 이름을 확인하세요.");
            }
            else
            {
                // makersImage = makersCanvas.transform.Find("MakersImage")?.GetComponent<Image>();
                makersBackButton = makersCanvas.Find("MakersBackButton")?.GetComponent<Button>();

                //if (makersImage == null)
                // Debug.LogError("MakersImage를 찾을 수 없습니다. Hierarchy에서 MakersImage 오브젝트의 이름을 확인하세요.");
                if (makersBackButton == null)
                    Debug.LogError("BackButton을 찾을 수 없습니다. Hierarchy에서 BackButton 오브젝트의 이름을 확인하세요.");
            }
            #endregion
            */

            /*
            #region GameInfo Ui
            if (gameInfoCanvas == null)
            {
                Debug.LogError("GameInfoCanvas를 찾을 수 없습니다. Hierarchy에서 GameInfoCanvas 오브젝트의 이름을 확인하세요.");
            }
            else
            {
                // gameInfoImage = gameInfoCanvas.transform.Find("GameInfoImage")?.GetComponent<Image>();
                gameInfoBackButton = gameInfoCanvas.Find("GameInfoBackButton")?.GetComponent<Button>();

               // if (gameInfoImage == null)
                  //  Debug.LogError("GameInfoImage를 찾을 수 없습니다. Hierarchy에서 GameInfoImage 오브젝트의 이름을 확인하세요.");
                if (gameInfoBackButton == null)
                    Debug.LogError("BackButton을 찾을 수 없습니다. Hierarchy에서 BackButton 오브젝트의 이름을 확인하세요.");
            }
            #endregion
            */

            #region Button Add Listener
            // 게임시작 -> Lobby 씬 전환 버튼 할당
            startButton.onClick.AddListener(() =>
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Lobby));

            // GameInfoButton과 MakersButton에 클릭 이벤트 추가
            // gameInfoButton.onClick.AddListener(() => HideCanvas(uiTitle));
            // makersButton.onClick.AddListener(() => HideCanvas(uiTitle));

            // 뒤로 가기 버튼에 클릭 이벤트 추가
            //gameInfoBackButton.onClick.AddListener(() => ShowCanvas(uiTitle, gameInfoCanvas));
            //makersBackButton.onClick.AddListener(() => ShowCanvas(uiTitle, makersCanvas));

           
            #endregion
        }

        void Start()
        {
            #region 정면 UI Animation
            //AnimateTitle();
            //AnimateSubTitle();
            AnimateButton(startButton, 1);
            // AnimateButton(gameInfoButton, 2);
            // AnimateButton(makersButton, 3);
            #endregion
        }

        void AnimateTitle()
        {
            if (titleText != null)
            {
                // 시작 위치와 Z축 설정
                titleText.transform.localPosition = new Vector3(titleText.transform.localPosition.x, titleText.transform.localPosition.y, 3);
                titleText.DOFade(1, 4).SetEase(Ease.InOutQuad);
                titleText.transform.DOLocalMoveZ(0, 1).SetEase(Ease.OutQuad); // Z축 애니메이션
            }
            /*
            if (titleText != null)
            {
                // titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0);
                titleText.DOFade(1, 1).SetEase(Ease.InOutQuad);
                titleText.rectTransform.anchoredPosition = new Vector2(-3, 0);
                titleText.rectTransform.DOAnchorPos(Vector2.zero, 3).SetEase(Ease.OutQuad);
                // titleText.rectTransform.DOShakePosition(1, new Vector3(1, 1, 0), 10, 90, false, true).SetDelay(2);
            }
            */
        }

        void AnimateSubTitle()
        {

            if (subTitleText != null)
            {
                // 시작 위치와 Z축 설정
                subTitleText.transform.localPosition = new Vector3(subTitleText.transform.localPosition.x, subTitleText.transform.localPosition.y, 3);
                subTitleText.DOFade(1, 4).SetEase(Ease.InOutQuad);
                subTitleText.transform.DOLocalMoveZ(0, 1).SetEase(Ease.OutQuad); // Z축 애니메이션
            }

            /*
            if (subTitleText != null)
            {
                // subTitleText.color = new Color(subTitleText.color.r, subTitleText.color.g, subTitleText.color.b, 0);
                subTitleText.DOFade(1, 2).SetEase(Ease.InOutQuad);
                subTitleText.rectTransform.anchoredPosition = new Vector2(-3, -5);
                subTitleText.rectTransform.DOAnchorPos(Vector2.zero, 2).SetEase(Ease.OutQuad);
                subTitleText.rectTransform.DOShakePosition(1, new Vector3(1, 1, 0), 3, 0, false, true).SetDelay(3);
            }
            */
        }

        void AnimateButton(Button button, float delay)
        {
            if (button != null)
            {
                // 현재 위치에 나타나는 애니메이션
                CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = button.gameObject.AddComponent<CanvasGroup>();
                }
                canvasGroup.alpha = 0;
                canvasGroup.DOFade(1, 10).SetEase(Ease.InOutQuad).SetDelay(delay);
            }
        }

        /*
        /// <summary>
        /// 버튼 클릭 시 현재 캔버스 안보이기
        /// </summary>
        /// <param name="currentCanvas"></param>
        void HideCanvas(Transform currentCanvas)
        {
            // 현재 UI 끄기 
            currentCanvas.gameObject.SetActive(false);

        }
        */
        void AnimateMakersImage()
        {
            /*
            if(makersImage != null)
            {
                // makersImage.transform.localScale = new Vector3(0, 0, 0); // 시작 크기 설정
                makersImage.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutBounce); // 크기 애니메이션
                makersImage.rectTransform.anchoredPosition = new Vector2(0, -500); // 시작 위치 설정
                makersImage.rectTransform.DOAnchorPos(Vector2.zero, 1).SetEase(Ease.OutBack); // 위치 애니메이션
            }
            */
        }
    }
}