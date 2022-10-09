using UnityEngine;

namespace Ballcade
{
    public static class CanvasGroupExtensions
    {
        /// <summary>Toggles visibility of the CanvasGroup.</summary>
        public static void Toggle(this CanvasGroup canvasGroup, bool? setValue = null)
        {
            if (setValue.HasValue)
            {
                if (setValue.Value)
                    canvasGroup.Show();
                else
                    canvasGroup.Hide();
            }
            else
            {
                if (!canvasGroup.IsVisible())
                    canvasGroup.Show();
                else
                    canvasGroup.Hide();
            }
        }

        /// <summary>Returns the visibility of a CanvasGroup.</summary>
        public static bool IsVisible(this CanvasGroup canvasGroup)
        {
            return canvasGroup.alpha == 1f;
        }

        /// <summary>Makes the CanvasGroup visible and interactable.</summary>
        public static void Show(this CanvasGroup canvasGroup, float? duration = null, float? delay = null,
            bool setInvisibleBefore = false)
        {
#if SURGE_TWEEN_AVAILABLE
			if (!(Application.isEditor && !Application.isPlaying)) {
				if (SetInvisibleBefore)
					CanvasGroup.HideImmediate(false);
				CanvasGroup.interactable = true;
				Tween.CanvasGroupAlpha(CanvasGroup, 1f, Duration.HasValue ? Duration.Value : ChangeDuration, Delay.HasValue ? Delay.Value : 0f, null, Tween.LoopType.None,
				                       () => CanvasGroup.gameObject.SetActive(true), null);
			} else
#endif
            canvasGroup.ShowImmediate();
        }

        /// <summary>Makes the CanvasGroup visible immediatly without transition.</summary>
        public static void ShowImmediate(this CanvasGroup canvasGroup)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
        }

        /// <summary>Hides the CanvasGroup.</summary>
        [ContextMenu("Hide")]
        public static void Hide(this CanvasGroup canvasGroup, float? duration = null, float? delay = null,
            bool setVisibleBefore = false, bool setInactive = true)
        {
            canvasGroup.HideImmediate(setInactive);
        }

        /// <summary>Hides the CanvasGroup, but keeps the GameObject active.</summary>
        public static void HideButKeepActive(this CanvasGroup canvasGroup, float? duration = null, float? delay = null,
            bool setVisibleBefore = false)
        {
            canvasGroup.Hide(duration, delay, setVisibleBefore, false);
        }

        public static void HideImmediate(this CanvasGroup canvasGroup, bool setInactive = true)
        {
            if (setInactive)
                canvasGroup.gameObject.SetActive(false);
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
        }
    }
}