using System;
using CoreLib.Complex_Types;
using CoreLib.Events;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CoreLib.Notifier
{
    public class PlayerNotificationPanel : MonoBehaviour, IPointerDownHandler, IPoolable
    {
        [SerializeField]TextMeshProUGUI Text;
        private Action callback;
        
        private float timeToBeDisplayed = 8f;
        public float time = 0;
        
        public void Setup(string text, bool isNegative = true, Action callback = null)
        {
            Text.text = text;
            Text.color = new Color32(55, 255, 0, 255);
            //o.effectColor = isNegative ? Color.red : Color.green;
            this.callback = callback;
            StaticEvents_Music.OnNotification.Invoke();

        }

        public void OnClick()
        {
            callback?.Invoke();
            Destroy(gameObject);
        }
        
        private void Update()
        {
            time += UnityEngine.Time.deltaTime;
            if (time > timeToBeDisplayed)
            {
                PlayerNotifier.NoticePool.Release(gameObject);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClick();
        }

        public void OnPoolCreate()
        {
        }

        public void OnPoolDeploy()
        {
        }

        public void OnPoolReturn()
        {
            time = 0;
            callback = null;
            Text.text = "";
        }

        public void OnPoolDestroy()
        {
        }
    }
}