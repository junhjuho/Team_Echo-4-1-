using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FuseboxSystem
{
    public class FBUIManager : MonoBehaviour
    {
        [Header("Fuse Inventory Canvas")]
        [SerializeField] private CanvasGroup fuseInventory = null;

        [Header("Fuse UI")]
        [SerializeField] private TMP_Text fuseAmountText = null;

        [Header("Crosshair")]
        [SerializeField] private Image crosshair = null;

        [Header("Should persist?")]
        [SerializeField] private bool persistAcrossScenes = true;

        private bool isOpen;

        public static FBUIManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                if (persistAcrossScenes)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            FieldNullCheck();
        }

        private void Update()
        {
            if (Input.GetKeyDown(FBInputManager.instance.inventoryKey))
            {
                OpenInventory();
            }
        }

        public void OpenInventory()
        {
            isOpen = !isOpen;
            fuseInventory.alpha = isOpen ? 1 : 0;
        }


        public void UpdateFuseUI(int fusesAmount)
        {
            fuseAmountText.text = fusesAmount.ToString("0");
        }

        public void CrosshairChange(bool on)
        {
            crosshair.color = on ? Color.red : Color.white;
        }

        void FieldNullCheck()
        {
            // Checking each field and logging an error if it is null
            CheckField(fuseInventory, "FuseInventory");
            CheckField(fuseAmountText, "FuseAmountText");
            CheckField(crosshair, "Crosshair");
        }

        void CheckField(Object field, string fieldName)
        {
            if (field == null)
            {
                Debug.LogError($"FieldNullCheck: {fieldName} is not set in the inspector!");
            }
        }
    }
}

