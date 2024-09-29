using System;
using CoreLib.Complex_Types;
using CoreLib.Extensions;
using UnityEngine;

namespace CoreLib.Notifier
{
    public class PlayerNotifier : MonoBehaviour
    {
        private static PlayerNotifier Instance;
        public static GoPool NoticePool;

        [Header("UI References")] 
        [SerializeField] private GameObject NotificationPanelPrefab;
        [SerializeField] private Transform NotificationPanelParent;


        private void Awake()
        {
            Instance = this;
            NoticePool = new GoPool(NotificationPanelPrefab, 4);
            NotifyPlayer("Notifier Online.");
        }

        public static void NotifyPlayer(string message, bool isNegative = false, Action OnOK = null)
        {
            if (Instance is null)
            {
                Debug.LogWarning("PlayerNotifier is not initialized.");
                return;
            }

            GameObject notificationPanel = NoticePool.Get();
            notificationPanel.transform.SetParent(Instance.NotificationPanelParent);
            notificationPanel.transform.localScale = Vector3.one;
            var panel = notificationPanel.GetComponent<PlayerNotificationPanel>();
            panel.Setup(message, isNegative, OnOK);
            panel.time += 1f * Instance.NotificationPanelParent.childCount;
            notificationPanel.transform.localPosition =  notificationPanel.transform.localPosition.ToZ(0);
        }
    }
}