using DG.Tweening;
using NHR;
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
        /// <summary>
        /// Text (Tween)
        /// </summary>
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

        // public Button characterSettingButton;
        // public Button gameSettingButton;

        // Ű �Է��� �ٸ� ������ �ѱ�
        //public UIKeyboard keyboard;

        private void Awake()
        {
            UIManager.Instance.titleSceneMenu = this;

            Transform uiTitle = GameObject.Find("UITitle")?.transform;

            if (uiTitle != null)
            {
                // ã�Ƽ� �ڵ� �Ҵ�
                this.titleText = transform.Find("TitleText")
                    .GetComponent<TMP_Text>();
                this.subTitleText = transform.Find("SubTitleText").GetComponent<TMP_Text>();
                this.startButton = uiTitle.Find("StartButton").GetComponent<Button>();
                this.gameInfoButton = uiTitle.Find("GameInfoButton").GetComponent<Button>();

                // tutorialButton = transform.Find("TutorialButton").GetComponent<Button>();
                // keyboard = FindObjectOfType<UIKeyboard>(); -> Ű���� �ٸ� ������ �ѱ�
                //characterSettingButton = transform.Find("CharacterSettingButton").GetComponent<Button>();
                //gameSettingButton = transform.Find("GameSettingButton").GetComponent<Button>();

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

            // �κ� �� ��ȯ ��ư �Ҵ�
            startButton.onClick.AddListener(() =>
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Lobby));

            // ���� ����
            gameInfoButton.onClick.AddListener(() =>
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.GameInfo));

            /* Ʃ�丮�� - 1���� ��ħ ( ���� ���� -> Ʃ�丮������� ��ȯ )
            tutorialButton.onClick.AddListener(() =>
            EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Tutorial));
            */

            /* ���
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

            AnimateTitle();
            AnimateSubTitle();
            AnimateStartButton();
            AnimateGameInfoButton();

        }

        void AnimateTitle()
        {
            if (titleText != null)
            {
                // Title �ؽ�Ʈ�� ������ ��Ÿ���� �ϰ�, �ణ�� ���� ȿ���� �߰�
                titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0);
                titleText.DOFade(1, 3).SetEase(Ease.InOutQuad); // 3�� ���� ���� ��ȭ
                titleText.rectTransform.anchoredPosition = new Vector2(0, 500);
                titleText.rectTransform.DOAnchorPos(Vector2.zero, 3).SetEase(Ease.OutQuad); // 3�� ���� ��ġ �̵�
                titleText.rectTransform.DOShakePosition(5, new Vector3(5, 5, 0), 10, 90, false, true).SetDelay(3); // 3�� �� 5�� ���� ����
            }
        }

        void AnimateSubTitle()
        {
            if (subTitleText != null)
            {
                // SubTitle �ؽ�Ʈ�� ������ ��Ÿ���� �ϰ�, �ణ�� ���� ȿ���� �߰�
                subTitleText.color = new Color(subTitleText.color.r, subTitleText.color.g, subTitleText.color.b, 0);
                subTitleText.DOFade(1, 3).SetEase(Ease.InOutQuad); // 3�� ���� ���� ��ȭ
                subTitleText.rectTransform.anchoredPosition = new Vector2(-500, 0);
                subTitleText.rectTransform.DOAnchorPos(Vector2.zero, 3).SetEase(Ease.OutQuad); // 3�� ���� ��ġ �̵�
                subTitleText.rectTransform.DOShakePosition(5, new Vector3(5, 5, 0), 10, 90, false, true).SetDelay(3); // 3�� �� 5�� ���� ����
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
                canvasGroup.DOFade(1, 3).SetEase(Ease.InOutQuad).SetDelay(1); // 1�� ���� �� 3�� ���� ���� ��ȭ
                startButton.transform.DOShakePosition(5, new Vector3(5, 5, 0), 10, 90, false, true).SetDelay(4); // 4�� �� 5�� ���� ����
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
                canvasGroup.DOFade(1, 3).SetEase(Ease.InOutQuad).SetDelay(2); // 2�� ���� �� 3�� ���� ���� ��ȭ
                gameInfoButton.transform.DOShakePosition(5, new Vector3(5, 5, 0), 10, 90, false, true).SetDelay(5); // 5�� �� 5�� ���� ����
            }
        }

    }

}
