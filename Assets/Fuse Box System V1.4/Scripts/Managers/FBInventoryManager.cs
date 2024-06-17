using UnityEngine;

namespace FuseboxSystem
{
    public class FBInventoryManager : MonoBehaviour
    {
        [Header("Fuses in Inventory")]
        [SerializeField] private int inventoryFuses;

        [Header("Should persist?")]
        [SerializeField] private bool persistAcrossScenes = true;

        public static FBInventoryManager instance;

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
        }

        public void AddFuse()
        {
            InventoryFuses++;
        }

        public void RemoveFuse()
        {
            InventoryFuses--;
        }

        public int InventoryFuses
        {
            get => inventoryFuses;
            set
            {
                inventoryFuses = value;
                FBUIManager.instance.UpdateFuseUI(inventoryFuses);
            }
        }
    }
}
