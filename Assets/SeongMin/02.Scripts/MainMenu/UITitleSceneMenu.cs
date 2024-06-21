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
    /// Ÿ��Ʋ�� - Do Tween���� ���� 
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
        // public Button tutorialButton;
        [Header("���� ���� UI")]
        public Button gameInfoButton;
        [Header("������ ���� UI")]
        public Button makersButton;
        #endregion

        #region ���� UI ǥ��

        #endregion

        #region ���� UI ǥ��

        #endregion

        // public Button characterSettingButton;
        // public Button gameSettingButton;

        // Ű �Է��� �ٸ� ������ �ѱ�
        //public UIKeyboard keyboard;

        private void Awake()
        {
            UIManager.Instance.titleSceneMenu = this;

            Transform uiTitle = GameObject.Find("UITitle")?.transform;
            Transform uiTitleRight = GameObject.Find("UITitleRight")?.transform;
            Transform uiTitleLeft = GameObject.Find("UITitleLeft")?.transform;

            if (uiTitle != null)
            {
                #region ���� UI
                this.titleText = transform.Find("TitleText").GetComponent<TMP_Text>();
                subTitleText = transform.Find("SubTitleText").GetComponent<TMP_Text>();

                startButton = uiTitle.Find("StartButton").GetComponent<Button>();
                gameInfoButton = uiTitle.Find("GameInfoButton").GetComponent<Button>();
                makersButton = uiTitle.Find("MakersButton").GetComponent<Button>();
                #endregion



            }
            else
            {
                Debug.LogError("UITitle Canvas�� ã�� �� �����ϴ�. Hierarchy���� UITitle ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }

            // ����ȭ�� ���ؼ� ã�� ������ �� ���� ó��
            if (titleText == null)
            {
                Debug.LogError("TitleText�� ã�� �� �����ϴ�. Hierarchy���� TitleText ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }

            if (subTitleText == null)
            {
                Debug.LogError("SubTitleText�� ã�� �� �����ϴ�. Hierarchy���� SubTitleText ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }

            if (startButton == null)
            {
                Debug.LogError("StartButton�� ã�� �� �����ϴ�. Hierarchy���� StartButton ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }

            if (gameInfoButton == null)
            {
                Debug.LogError("GameInfoButton�� ã�� �� �����ϴ�. Hierarchy���� StartButton ������Ʈ�� �̸��� Ȯ���ϼ���.");

            }

            // ���ӽ��� -> Ʃ�丮�� �� ��ȯ ��ư �Ҵ�, 
            startButton.onClick.AddListener(() =>
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Tutorial));

            /*
            gameInfoButton.onClick.AddListener(() =>
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.GameInfo));

            // Ʃ�丮�� - 1���� ��ħ ( ���� ���� -> Ʃ�丮������� ��ȯ )
            tutorialButton.onClick.AddListener(() =>
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Tutorial));
            

             ���
            characterSettingButton.onClick.AddListener(() => 
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.CharacterCustom));
            */

        }

        void Start()
        {
            /* Awake���� check �ѹ� �����ϱ� �ʿ������
            if (titleText != null)
            {
                AnimateTitle();
            }
            */

            #region ���� UI Animation
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
                // Title �ؽ�Ʈ�� ������ ��Ÿ���� �ϰ�, �ణ�� ���� ȿ���� �߰�
                titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0);
                titleText.DOFade(1, 1).SetEase(Ease.InOutQuad);
                titleText.rectTransform.anchoredPosition = new Vector2(0, 500);
                titleText.rectTransform.DOAnchorPos(Vector2.zero, 3).SetEase(Ease.OutQuad); // 3�� ���� ��ġ �̵�
                // duration, strength, vibrato, randomness, snapping, fadeout
                titleText.rectTransform.DOShakePosition(3, new Vector3(3, 3, 10), 10, 90, false, true).SetDelay(2); // 3�� �� 5�� ���� ����
            }
        }

        void AnimateSubTitle()
        {
            if (subTitleText != null)
            {
                // SubTitle �ؽ�Ʈ�� ������ ��Ÿ���� �ϰ�, �ణ�� ���� ȿ���� �߰�
                subTitleText.color = new Color(subTitleText.color.r, subTitleText.color.g, subTitleText.color.b, 0);
                subTitleText.DOFade(1, 2).SetEase(Ease.InOutQuad); // 3�� ���� ���� ��ȭ
                subTitleText.rectTransform.anchoredPosition = new Vector2(-500, 0);
                subTitleText.rectTransform.DOAnchorPos(Vector2.zero, 3).SetEase(Ease.OutQuad); // 3�� ���� ��ġ �̵�
                subTitleText.rectTransform.DOShakePosition(3, new Vector3(3, 3, 10), 10, 90, false, true).SetDelay(2);
            }
        }

        void AnimateStartButton()
        {
            if (startButton != null)
            {
                // Start ��ư�� ������ ��Ÿ���� �ϰ�, �ణ�� ���� ȿ���� �߰�
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
                // GameInfo ��ư�� ������ ��Ÿ���� �ϰ�, �ణ�� ���� ȿ���� �߰�
                CanvasGroup canvasGroup = gameInfoButton.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = gameInfoButton.gameObject.AddComponent<CanvasGroup>();
                }
                canvasGroup.alpha = 0;
                canvasGroup.DOFade(1, 2).SetEase(Ease.InOutQuad).SetDelay(2); // 2�� ���� �� 3�� ���� ���� ��ȭ
                gameInfoButton.transform.DOShakePosition(1, new Vector3(5, 5, 0), 10, 90, false, true).SetDelay(1);
            }
        }

        void AnimateMakersButton()
        {
            if(makersButton != null)
            {
                // MakersButton ��ư�� ������ ��Ÿ���� �ϰ�, �ణ�� ���� ȿ���� �߰�
                CanvasGroup canvasGroup = gameInfoButton.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = gameInfoButton.gameObject.AddComponent<CanvasGroup>();
                }
                canvasGroup.alpha = 0;
                canvasGroup.DOFade(1, 2).SetEase(Ease.InOutQuad).SetDelay(2); // 2�� ���� �� 3�� ���� ���� ��ȭ
                gameInfoButton.transform.DOShakePosition(1, new Vector3(5, 5, 0), 10, 90, false, true).SetDelay(1);

            }
        }
    }

}
