using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TezUI
{
    /// <summary>
    /// Provides effects for manipulating <see cref="Text"/> components.
    /// </summary>
    public static class TextExtensions
    {
        /// <summary>
        /// Sets the font size of the provided text to the specified font size 
        /// gradually over time.
        /// </summary>
        /// <param name="text">The text that's font size will change.</param>
        /// <param name="fontSize">The new font size to change to.</param>
        /// <param name="time">The amount of time the effect takes.</param>
        /// <returns>Waits until the text reaches the new font size.</returns>
        public static Coroutine SetFontSize(this Text text, int fontSize, float time)
        {
            var coroutine = text.StartCoroutine(SetFontSizeCoroutine());

            GraphicManager.Add(text, EffectType.FontSize, coroutine);

            return coroutine;

            IEnumerator SetFontSizeCoroutine()
            {
                var distance = Mathf.Abs(text.fontSize - fontSize);

                while (text.fontSize != fontSize)
                {
                    yield return new WaitForEndOfFrame();

                    var previousFontSize = text.fontSize;
                    text.fontSize = Mathf.RoundToInt(Mathf.MoveTowards(text.fontSize, fontSize, Utils.GetDistanceFromTime(time, distance)));

                    // Font size didn't change :(
                    if (previousFontSize == text.fontSize)
                    {
                        if (fontSize > text.fontSize)
                        {
                            text.fontSize++;
                        }
                        else
                        {
                            text.fontSize--;
                        }
                    }
                }

                GraphicManager.Remove(text, EffectType.FontSize);
            }
        }
    }
}
