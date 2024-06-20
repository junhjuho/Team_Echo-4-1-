using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using SeongMin;

public class TitleSceneController : MonoBehaviour
{
    [Header("제목")]
    public Text titleText;
    [Header("부 제목")]
    public Text subTitleText;
    [Header("Start 버튼 (UI)")]
    public Button startButton;
    [Header("Setting 버튼 (UI) - 임시")]
    public Button settingButton;

    public Canvas canvas;

    /// <summary>
    /// tweening 최적화 실행 및 할당
    /// </summary>
    void Awake()
    {
        UIManager.Instance.titleSceneController = this;

        // TitleText와 StartButton을 이름으로 찾아 할당
        titleText = this.canvas.GetComponentInChildren<Text>();
        subTitleText = GameObject.Find("SubTitleText").GetComponent<Text>();
        
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        settingButton = GameObject.Find("SettingButton").GetComponent<Button>();

        Transform uiTitle = GameObject.Find("UITitle").transform;

        // 최적화를 위해서 찾지 못했을 때 오류 처리
        if (titleText == null)
        {
            Debug.LogError("TitleText를 찾을 수 없습니다. Hierarchy에서 TitleText 오브젝트의 이름을 확인하세요.");
        }

        if (subTitleText == null)
        {
            Debug.LogError("TitleText를 찾을 수 없습니다. Hierarchy에서 TitleText 오브젝트의 이름을 확인하세요.");
        }

        if (startButton == null)
        {
            Debug.LogError("StartButton을 찾을 수 없습니다. Hierarchy에서 StartButton 오브젝트의 이름을 확인하세요.");
        }

        if (settingButton == null)
        {
            Debug.LogError("StartButton을 찾을 수 없습니다. Hierarchy에서 StartButton 오브젝트의 이름을 확인하세요.");
        }
    }

    void Start()
    {
        // 애니메이션 실행
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
        // Title 텍스트를 위에서 아래로 떨어지는 애니메이션
        titleText.rectTransform.anchoredPosition = new Vector2(0, 500);
        titleText.DOFade(1, 1); // 1초 동안 투명도 변화
        titleText.rectTransform.DOAnchorPos(Vector2.zero, 1).SetEase(Ease.OutBounce); // 1초 동안 위치 이동
    }

    void AnimateStartButton()
    {
        // Start 버튼을 점점 나타나게 하는 애니메이션
        CanvasGroup canvasGroup = startButton.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = startButton.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 1).SetDelay(1); // 1초 지연 후 1초 동안 투명도 변화
    }
}
