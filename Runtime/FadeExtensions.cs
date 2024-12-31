using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TezUI
{
    /// <summary>
    /// Provides methods for fading graphic colors over time.
    /// </summary>
    public static class FadeExtensions
    {
        private static readonly HashSet<Graphic> _glowingGraphics = new HashSet<Graphic>();

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

        /// <summary>
        /// Causes the graphic to start fading in and out using the specified
        /// alpha. Use <see cref="StopGlowing"/> on the graphic end the effect.
        /// </summary>
        /// <param name="graphic">The graphic which will start glowing.</param>
        /// <param name="time">The time it takes for the graphic to fade.</param>
        /// <param name="alpha">The destination alpha, which should lower than
        /// the input.</param>
        public static void Glow(this Graphic graphic, float time, float alpha)
        {
	        if (!graphic.isActiveAndEnabled)
	        {
		        Debug.LogWarning($"Can't run method {nameof(Glow)} on {graphic.name}, it is disabled.", graphic);

		        return;
	        }

	        if (_glowingGraphics.Contains(graphic))
	        {
		        Debug.LogWarning($"Can't run {nameof(Glow)} on {graphic.name}, it is already glowing.", graphic);

		        return;
			}

	        _glowingGraphics.Add(graphic);

			var initialColor = graphic.color;
	        var destinationColor = graphic.color.RGB(alpha);

	        var coroutine = graphic.StartCoroutine(GlowCoroutine(graphic, initialColor, destinationColor, time));
	        GraphicManager.Add(graphic, EffectType.SetColor, coroutine);
        }

        /// <summary>
        /// If a graphic is glowing using <see cref="Glow"/>, this will stop the effect.
        /// </summary>
        /// <param name="graphic">The graphic to stop glowing.</param>
        public static void StopGlowing(this Graphic graphic)
        {
	        if (!_glowingGraphics.Contains(graphic))
	        {
		        Debug.LogWarning($"Can't run {nameof(StopGlowing)} on {graphic.name}, it is not glowing.", graphic);

		        return;
	        }
            
	        _glowingGraphics.Remove(graphic);
            GraphicManager.Remove(graphic, EffectType.SetColor);
	        graphic.color = graphic.color.RGB(1f);
		}

        /// <summary>
        /// Returns whether or not a graphic is glowing.
        /// </summary>
        /// <param name="graphic">The graphic to check.</param>
        /// <returns>True if the graphic is glowing.</returns>
        public static bool IsGlowing(this Graphic graphic)
        {
            return _glowingGraphics.Contains(graphic);
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

        private static IEnumerator GlowCoroutine(Graphic graphic, Color initialColor, Color destinationColor, float time)
        {
	        while (_glowingGraphics.Contains(graphic))
	        {
				while (graphic.color != destinationColor)
				{
					yield return new WaitForEndOfFrame();

					graphic.color = Vector4.MoveTowards(graphic.color, destinationColor, Utils.GetDistanceFromTime(time));
				}

				yield return new WaitForEndOfFrame();

				while (graphic.color != initialColor)
				{
					yield return new WaitForEndOfFrame();

					graphic.color = Vector4.MoveTowards(graphic.color, initialColor, Utils.GetDistanceFromTime(time));
				}

				yield return new WaitForEndOfFrame();
			}
        }
    }
}
