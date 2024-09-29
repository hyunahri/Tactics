using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoreLib.Complex_Types;
using UnityEngine;
using UnityEngine.UI;

namespace CoreLib.Utilities
{
    public class SpriteAnimator : MonoBehaviour, IPoolable
    {
     //   public static CoreEvent ForceSyncAll = new CoreEvent();
        
        [Header("Animation Settings")] 
        [SerializeField] private List<Sprite> sprites = new List<Sprite>();
        private WaitForSecondsRealtime frameDelay;

        [SerializeField]private float timeBetweenFrames = 0.1f;
        public Action OnFinishedAnimating;

        public float TimeBetweenFrames
        {
            get => timeBetweenFrames;
            set
            {
                timeBetweenFrames = value;
                frameDelay = new WaitForSecondsRealtime(timeBetweenFrames);
            }
        }
        [NonSerialized]public bool loop = true;

        private Image imageComponent;
        private SpriteRenderer SR;
        
        public int currentSpriteIndex = 0;
        private Coroutine animationCoroutine;

        private void ForceSync()
        {
            if (animationCoroutine != null)
                CoroutineService.StopCoroutine(animationCoroutine);
            currentSpriteIndex = 0;
            frameDelay = new WaitForSecondsRealtime(timeBetweenFrames);
            if (sprites.Any() && gameObject.activeInHierarchy)
                animationCoroutine = CoroutineService.RunCoroutine(AnimateSprites());
        }

        private void Start()
        {
            //StaticEventsCampaign.ForceAnimatorSync.AddListener(ForceSync);
            frameDelay = new WaitForSecondsRealtime(timeBetweenFrames);
        }

        private void OnEnable()
        {
            //ForceSyncAll.AddListener(ForceSync);
            imageComponent ??= GetComponent<Image>();
            SR ??= GetComponent<SpriteRenderer>();
            frameDelay ??= new WaitForSecondsRealtime(timeBetweenFrames);
            if (sprites.Any())
                animationCoroutine = CoroutineService.RunCoroutine(AnimateSprites());
            //ForceSync();
        }

        private void OnDisable()
        {
            if (animationCoroutine == null) return;
            CoroutineService.StopCoroutine(animationCoroutine);
            animationCoroutine = null;
            //OnFinishedAnimating = null;
            //   ForceSyncAll.RemoveListener(ForceSync);
        }
        

        public void LoadSprites(List<Sprite> _sprites) => LoadSprites(_sprites.ToArray());
        public void LoadSprite(Sprite sprite) => LoadSprites(new[] { sprite });

        private void LoadSprites(Sprite[] _sprites)
        {
            Clear();
            sprites.AddRange(_sprites);
            if(currentSpriteIndex >= sprites.Count)
                currentSpriteIndex = 0;
            frameDelay = new WaitForSecondsRealtime(timeBetweenFrames);
            animationCoroutine = CoroutineService.RunCoroutine(AnimateSprites());
            if (SR) SR.enabled = true;
            if (imageComponent) imageComponent.enabled = true;
        }

        public void SetCompleteAnimationTime(float time)
        {
            timeBetweenFrames = time / sprites.Count;
            frameDelay = new WaitForSecondsRealtime(timeBetweenFrames);
        }

        public void Clear()
        {
            sprites.Clear();
            //currentSpriteIndex = 0;
            if (animationCoroutine != null)
                CoroutineService.StopCoroutine(animationCoroutine);
        }

        IEnumerator AnimateSprites()
        {
            while (true)
            {
                if(currentSpriteIndex >= sprites.Count)
                    currentSpriteIndex = 0;
                if (sprites.Count == 0)
                    yield break;
                Sprite activeSprite = sprites[currentSpriteIndex];

                if (SR) SR.sprite = activeSprite;
                if (imageComponent) imageComponent.sprite = activeSprite;

                currentSpriteIndex++;

                if (currentSpriteIndex >= sprites.Count)
                {
                    if (loop)
                    {
                        currentSpriteIndex = 0;
                    }
                    else
                    {
                        break; // Exit the loop and stop the animation if not looping
                    }
                }
                if (currentSpriteIndex >= sprites.Count)
                    currentSpriteIndex = 0;
                yield return frameDelay;
            }
            OnFinishedAnimating?.Invoke();
        }

        public void OnPoolCreate()
        { }

        public void OnPoolDeploy()
        { }
        
        public void OnPoolReturn()
        {
            Clear();
        }

        public void OnPoolDestroy()
        {
        }
    }
}