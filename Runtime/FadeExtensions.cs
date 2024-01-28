using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TezUI
{
    /// <summary>
    /// Provides methods for fading graphic colors over time.
    /// </summary>
    public static class FadeExtensions
    {
        /// <summary>
        /// Fade's a graphic's alpha in over the specified amount of time.
        /// </summary>
        /// <param name="graphic">The graphic to fade in.</param>
        /// <param name="time">The amount of time the fade in effect will 
        /// take.</param>
        /// <returns>Waits until the fade in is complete.</returns>
        public static WaitForFade FadeIn(this Graphic graphic, float time)
        {
            if (!graphic.isActiveAndEnabled)
            {
                Debug.LogWarning($"Can't run method {nameof(FadeIn)} on {graphic.name}, it is disabled.", graphic);

                return null;
            }

            var destinationColor = graphic.color.RGB(1f);
            if (graphic.color == destinationColor)
            {
                return null;
            }

            var coroutine = graphic.StartCoroutine(FadeCoroutine(graphic, destinationColor, time));

            GraphicManager.Add(graphic, EffectType.SetColor, coroutine);

            return new WaitForFade(graphic, destinationColor);
        }

        /// <summary>
        /// Fades a graphic's alpha out over the specified amount of time.
        /// </summary>
        /// <param name="graphic">The graphic to fade out.</param>
        /// <param name="time">The amount of time the fade effect will take.
        /// </param>
        /// <returns>Waits until the fade out is complete.</returns>
        public static WaitForFade FadeOut(this Graphic graphic, float time)
        {
			if (!graphic.isActiveAndEnabled)
			{
				Debug.LogWarning($"Can't run method {nameof(FadeOut)} on {graphic.name}, it is disabled.", graphic);

				return null;
			}

			var destinationColor = graphic.color.RGB(0f);
            var coroutine = graphic.StartCoroutine(FadeCoroutine(graphic, destinationColor, time));

            GraphicManager.Add(graphic, EffectType.SetColor, coroutine);

            return new WaitForFade(graphic, destinationColor);
        }

        /// <summary>
        /// Fades the graphic to a specific color over the specified amount of 
        /// time.
        /// </summary>
        /// <param name="graphic">The graphic to fade.</param>
        /// <param name="time">The time the effect will take.</param>
        /// <param name="color">The color to fade to.</param>
        /// <returns>Waits until the fade is complete.</returns>
        public static WaitForFade Fade(this Graphic graphic, float time, Color color)
        {
			if (!graphic.isActiveAndEnabled)
			{
				Debug.LogWarning($"Can't run method {nameof(Fade)} on {graphic.name}, it is disabled.", graphic);

				return null;
			}

			var coroutine = graphic.StartCoroutine(FadeCoroutine(graphic, color, time));

            GraphicManager.Add(graphic, EffectType.SetColor, coroutine);

            return new WaitForFade(graphic, color);
        }

        private static IEnumerator FadeCoroutine(Graphic graphic, Color color, float time)
        {
            while (graphic.color != color)
            {
                yield return new WaitForEndOfFrame();

                graphic.color = Vector4.MoveTowards(graphic.color, color, Utils.GetDistanceFromTime(time));
            }

            yield return new WaitForEndOfFrame();

            GraphicManager.Remove(graphic, EffectType.SetColor);
        }
    }
}
