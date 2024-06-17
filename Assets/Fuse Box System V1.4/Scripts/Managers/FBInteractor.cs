using UnityEngine;

namespace FuseboxSystem
{
    public class FBInteractor : MonoBehaviour
    {
        [Header("Raycast Distance")]
        [SerializeField] private int interactDistance = 5;

        [Header("ObjectTag")]
        [SerializeField] private string InteractiveTag = "InteractiveObject";

        private FuseItem raycastedObj;
        private Camera _camera;

        private void Awake()
        {
            if (!TryGetComponent<Camera>(out _camera))
            {
                Debug.LogError("Camera component not found on the GameObject.");
            }
        }

        void Update()
        {
            if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, interactDistance))
            {
                var selectedItem = hit.collider.GetComponent<FuseItem>();
                if (selectedItem != null && selectedItem.CompareTag(InteractiveTag))
                {
                    raycastedObj = selectedItem;
                    CrosshairChange(true);
                }
                else
                {
                    ClearRaycast();
                }
            }
            else
            {
                ClearRaycast();
            }

            if (raycastedObj != null)
            {
                if (Input.GetKeyDown(FBInputManager.instance.interactKey))
                {
                    raycastedObj.ObjectInteract();
                }
            }
        }
        private void ClearRaycast()
        {
            if (raycastedObj != null)
            {
                CrosshairChange(false);
                raycastedObj = null;
            }
        }

        void CrosshairChange(bool on)
        {
            FBUIManager.instance.CrosshairChange(on);
        }
    }
}
