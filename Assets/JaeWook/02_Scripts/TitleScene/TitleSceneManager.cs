using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using SeongMin;

namespace Jaewook
{
    public class TitleSceneController : MonoBehaviour
    {
        [Header("����")]
        public Text titleText;
        [Header("�� ����")]
        public Text subTitleText;
        [Header("Start ��ư (UI)")]
        public Button startButton;
        [Header("Setting ��ư (UI) - �ӽ�")]
        public Button settingButton;
        [Header("��ü Canvas")]
        public Canvas canvas;

        /// <summary>
        /// tweening ����ȭ ���� �� �Ҵ�
        /// </summary>
        void Awake()
        {
            UIManager.Instance.titleSceneController = this;
            Transform uiTitle = GameObject.Find("UITitle").transform;

            // TitleText�� StartButton�� �̸����� ã�� �Ҵ�
            this.titleText = transform.Find("TitleText").GetComponent<Text>();
            this.subTitleText = transform.Find("SubTitleText").GetComponent<Text>();

            this.startButton = transform.Find("StartButton").GetComponent<Button>();
            this.settingButton = transform.Find("SettingButton").GetComponent<Button>();


            // ����ȭ�� ���ؼ� ã�� ������ �� ���� ó��
            if (titleText == null)
            {
                Debug.LogError("TitleText�� ã�� �� �����ϴ�. Hierarchy���� TitleText ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }

            if (subTitleText == null)
            {
                Debug.LogError("TitleText�� ã�� �� �����ϴ�. Hierarchy���� TitleText ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }

            if (startButton == null)
            {
                Debug.LogError("StartButton�� ã�� �� �����ϴ�. Hierarchy���� StartButton ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }

            if (settingButton == null)
            {
                Debug.LogError("StartButton�� ã�� �� �����ϴ�. Hierarchy���� StartButton ������Ʈ�� �̸��� Ȯ���ϼ���.");
            }
        }

        void Start()
        {
            // �ִϸ��̼� ����
            if (titleText != null)
            {
                AnimateTitle();
            }

            if (startButton != null)
            {
                AnimateStartButton();
            }
        }

        void AnimateTitle()
        {
            // Title �ؽ�Ʈ�� ������ �Ʒ��� �������� �ִϸ��̼�
            titleText.rectTransform.anchoredPosition = new Vector2(0, 500);
            titleText.DOFade(1, 1); // 1�� ���� ���� ��ȭ
            titleText.rectTransform.DOAnchorPos(Vector2.zero, 1).SetEase(Ease.OutBounce); // 1�� ���� ��ġ �̵�
        }

        void AnimateStartButton()
        {
            // Start ��ư�� ���� ��Ÿ���� �ϴ� �ִϸ��̼�
            CanvasGroup canvasGroup = startButton.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = startButton.gameObject.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, 1).SetDelay(1); // 1�� ���� �� 1�� ���� ���� ��ȭ
        }
    }

}
