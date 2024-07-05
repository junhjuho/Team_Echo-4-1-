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
    /// Ÿ��Ʋ��
    /// </summary>
    public class UITitleSceneMenu : MonoBehaviour
    {
        #region ���� UI ǥ��
        [Header("���� ���� 4-1")]
        public TMP_Text titleText;
        [Header("���� ���� Find and Escape")]
        public TMP_Text subTitleText;

        /// <summary>
        /// Button 
        /// </summary>
        [Header("���ӽ�ŸƮ -> Ʃ�丮������� ��ȯ")]
        public Button startButton;
        [Header("���� ���� UI")]
        public Button gameInfoButton;
        [Header("������ ���� UI")]
        public Button makersButton;
        // public Button tutorialButton;
        #endregion

        /*
        #region ���� UI ǥ��
        [Header("����ĵ���� ���� Ÿ��Ʋ")]
        public TMP_Text titleTextRight;
        [Header("-")]
        public TMP_Text subTitleTextRight;
        #endregion

        #region ���� UI ǥ�� -> ���� 
        [Header("����ĵ���� ���� Ÿ��Ʋ")]
        public TMP_Text titleTextLeft;
        [Header("-")]
        public TMP_Text subTitleTextLeft;
        #endregion
        */

        // public Canvas makersCanvas; // ������ ���� UI ĵ����
        // public Image makersImage;
        public Button makersBackButton;

        // public Canvas gameInfoCanvas; // ���� ���� UI ĵ����
        // public Image gameInfoImage;
        public Button gameInfoBackButton;

        // Ű �Է��� �ٸ� ������ �ѱ�
        //public UIKeyboard keyboard;

        public void Awake()
        {
            UIManager.Instance.titleSceneMenu = this;

            Transform uiTitle = GameObject.Find("UITitle")?.transform;
            //Transform uiTitleRight = GameObject.Find("UITitleRight")?.transform;
            //Transform uiTitleLeft = GameObject.Find("UITitleLeft")?.transform;

            // Transform makersCanvas = GameObject.Find("MakersCanvas")?.transform;
            // Transform gameInfoCanvas = GameObject.Find("GameInfoCanvas")?.transform;

            
            #region ���� UI
            if (uiTitle == null)
            {
                Debug.LogError("UITitle Canvas�� ã�� �� �����ϴ�. Hierarchy���� UITitle ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
            else
            {
                titleText = uiTitle.Find("TitleText")?.GetComponent<TMP_Text>();
                subTitleText = uiTitle.Find("SubTitleText")?.GetComponent<TMP_Text>();

                startButton = uiTitle.Find("StartButton")?.GetComponent<Button>();
                gameInfoButton = uiTitle.Find("GameInfoButton")?.GetComponent<Button>();
                makersButton = uiTitle.Find("MakersButton")?.GetComponent<Button>();

                if (titleText == null)
                    Debug.LogError("TitleText�� ã�� �� �����ϴ�. Hierarchy���� TitleText ������Ʈ�� �̸��� Ȯ���ϼ���.");
                if (subTitleText == null)
                    Debug.LogError("SubTitleText�� ã�� �� �����ϴ�. Hierarchy���� SubTitleText ������Ʈ�� �̸��� Ȯ���ϼ���.");
                if (startButton == null)
                    Debug.LogError("StartButton�� ã�� �� �����ϴ�. Hierarchy���� StartButton ������Ʈ�� �̸��� Ȯ���ϼ���.");
                if (gameInfoButton == null)
                    Debug.LogError("GameInfoButton�� ã�� �� �����ϴ�. Hierarchy���� StartButton ������Ʈ�� �̸��� Ȯ���ϼ���.");
                if (makersButton == null)
                    Debug.LogError("MakersButton�� ã�� �� �����ϴ�. Hierarchy���� StartButton ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
                #endregion
            /*
            #region ���� UI
            if (uiTitleRight == null)
            {
                Debug.LogError("UITitleRight Canvas�� ã�� �� �����ϴ�. Hierarchy���� UITitleRight ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
            else
            {
                titleTextRight = uiTitleRight.Find("TitleTextRight")?.GetComponent<TMP_Text>();
                subTitleTextRight = uiTitleRight.Find("SubTitleTextRight")?.GetComponent<TMP_Text>();

                if (titleTextRight == null)
                    Debug.LogError("TitleTextRight�� ã�� �� �����ϴ�. Hierarchy���� TitleTextRight ������Ʈ�� �̸��� Ȯ���ϼ���.");
                if (subTitleTextRight == null)
                    Debug.LogError("SubTitleTextRight�� ã�� �� �����ϴ�. Hierarchy���� SubTitleTextRight ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
                #endregion
            
            #region ���� UI
            if (uiTitleLeft == null)
            {
                Debug.LogError("UITitleLeft Canvas�� ã�� �� �����ϴ�. Hierarchy���� UITitleLeft ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
            else
            {
                titleTextLeft = uiTitleLeft.Find("TitleTextLeft")?.GetComponent<TMP_Text>();
                subTitleTextLeft = uiTitleLeft.Find("SubTitleTextLeft")?.GetComponent<TMP_Text>();

                if (titleTextLeft == null)
                    Debug.LogError("TitleTextLeft�� ã�� �� �����ϴ�. Hierarchy���� TitleTextLeft ������Ʈ�� �̸��� Ȯ���ϼ���.");
                if (subTitleTextLeft == null)
                    Debug.LogError("SubTitleTextLeft�� ã�� �� �����ϴ�. Hierarchy���� SubTitleTextLeft ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
            #endregion
            */

            /*
            #region Makers Ui
            if (makersCanvas == null)
            {
                Debug.LogError("MakersCanvas�� ã�� �� �����ϴ�. Hierarchy���� MakersCanvas ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
            else
            {
                // makersImage = makersCanvas.transform.Find("MakersImage")?.GetComponent<Image>();
                makersBackButton = makersCanvas.Find("MakersBackButton")?.GetComponent<Button>();

                //if (makersImage == null)
                // Debug.LogError("MakersImage�� ã�� �� �����ϴ�. Hierarchy���� MakersImage ������Ʈ�� �̸��� Ȯ���ϼ���.");
                if (makersBackButton == null)
                    Debug.LogError("BackButton�� ã�� �� �����ϴ�. Hierarchy���� BackButton ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
            #endregion
            */

            /*
            #region GameInfo Ui
            if (gameInfoCanvas == null)
            {
                Debug.LogError("GameInfoCanvas�� ã�� �� �����ϴ�. Hierarchy���� GameInfoCanvas ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
            else
            {
                // gameInfoImage = gameInfoCanvas.transform.Find("GameInfoImage")?.GetComponent<Image>();
                gameInfoBackButton = gameInfoCanvas.Find("GameInfoBackButton")?.GetComponent<Button>();

               // if (gameInfoImage == null)
                  //  Debug.LogError("GameInfoImage�� ã�� �� �����ϴ�. Hierarchy���� GameInfoImage ������Ʈ�� �̸��� Ȯ���ϼ���.");
                if (gameInfoBackButton == null)
                    Debug.LogError("BackButton�� ã�� �� �����ϴ�. Hierarchy���� BackButton ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
            #endregion
            */

            #region Button Add Listener
            // ���ӽ��� -> Lobby �� ��ȯ ��ư �Ҵ�
            startButton.onClick.AddListener(() =>
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Lobby));

            // GameInfoButton�� MakersButton�� Ŭ�� �̺�Ʈ �߰�
            // gameInfoButton.onClick.AddListener(() => HideCanvas(uiTitle));
            // makersButton.onClick.AddListener(() => HideCanvas(uiTitle));

            // �ڷ� ���� ��ư�� Ŭ�� �̺�Ʈ �߰�
            //gameInfoBackButton.onClick.AddListener(() => ShowCanvas(uiTitle, gameInfoCanvas));
            //makersBackButton.onClick.AddListener(() => ShowCanvas(uiTitle, makersCanvas));

           
            #endregion
        }

        void Start()
        {
            #region ���� UI Animation
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
                // ���� ��ġ�� Z�� ����
                titleText.transform.localPosition = new Vector3(titleText.transform.localPosition.x, titleText.transform.localPosition.y, 3);
                titleText.DOFade(1, 4).SetEase(Ease.InOutQuad);
                titleText.transform.DOLocalMoveZ(0, 1).SetEase(Ease.OutQuad); // Z�� �ִϸ��̼�
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
                // ���� ��ġ�� Z�� ����
                subTitleText.transform.localPosition = new Vector3(subTitleText.transform.localPosition.x, subTitleText.transform.localPosition.y, 3);
                subTitleText.DOFade(1, 4).SetEase(Ease.InOutQuad);
                subTitleText.transform.DOLocalMoveZ(0, 1).SetEase(Ease.OutQuad); // Z�� �ִϸ��̼�
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
                // ���� ��ġ�� ��Ÿ���� �ִϸ��̼�
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
        /// ��ư Ŭ�� �� ���� ĵ���� �Ⱥ��̱�
        /// </summary>
        /// <param name="currentCanvas"></param>
        void HideCanvas(Transform currentCanvas)
        {
            // ���� UI ���� 
            currentCanvas.gameObject.SetActive(false);

        }
        */
        void AnimateMakersImage()
        {
            /*
            if(makersImage != null)
            {
                // makersImage.transform.localScale = new Vector3(0, 0, 0); // ���� ũ�� ����
                makersImage.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutBounce); // ũ�� �ִϸ��̼�
                makersImage.rectTransform.anchoredPosition = new Vector2(0, -500); // ���� ��ġ ����
                makersImage.rectTransform.DOAnchorPos(Vector2.zero, 1).SetEase(Ease.OutBack); // ��ġ �ִϸ��̼�
            }
            */
        }
    }
}