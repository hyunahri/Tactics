using System;
using CoreLib.Events;
using TMPro;
using UnityEngine;

namespace CoreLib.Utilities
{
    public class ConfirmationMenuController : MonoBehaviour
    {
        public static ConfirmationMenuController Instance;
    
        private bool pendingDecision = false;
    
        [SerializeField] private GameObject ConfirmationMenu;
        [SerializeField] private TextMeshProUGUI ConfirmationText;

        [SerializeField] private TextMeshProUGUI ConfirmButtonText;
        [SerializeField] private TextMeshProUGUI CancelButtonText;
    
        private Action onConfirm;
        private Action onCancel;
    
        private void Awake()
        {
            Instance = this;
            ConfirmationMenu.SetActive(false);
            pendingDecision = false;
        }

        public void RequestConfirmation(string text, Action _onConfirm, Action _onCancel = null, string confirmText = "Confirm", string cancelText = "Cancel")
        {
            if(pendingDecision)
                Cancel();
            ConfirmationText.text = text;
            onConfirm = _onConfirm;
            onCancel = _onCancel;
            ConfirmationMenu.SetActive(true);
            StaticEvents_Music.OnNotification.Invoke();
            pendingDecision = true;
            ConfirmButtonText.text = confirmText;
            CancelButtonText.text = cancelText;
        }

        private void Update()
        {
            if(ConfirmationMenu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
                Cancel();
        }

        //Button Hooks
        public void Confirm()
        {
            StaticEvents_Music.OnActionClick.Invoke();
            onConfirm?.Invoke();
            ConfirmationMenu.SetActive(false);
            pendingDecision = false;
        }
    
        public void Cancel()
        {
            StaticEvents_Music.OnActionClick.Invoke();
            onCancel?.Invoke();
            ConfirmationMenu.SetActive(false);
            pendingDecision = false;
        }
    }
}
