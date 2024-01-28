using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TezUI
{
    internal static class Utils
    {
        /// <summary>
        /// Given a time in seconds, this will return the distance between two 
        /// frames required to move or lerp from one value to another based
        /// on the frame rate in the amount of time.
        /// </summary>
        /// <param name="time">An amount of time in seconds.</param>
        /// <param name="distance">The distance being moved.</param>
        /// <returns>A distance to move ot lerp each frame.</returns>
        public static float GetDistanceFromTime(float time, float distance = 1f)
        {
            var framesInTime = time * (1f / Time.deltaTime);

            return distance / framesInTime;
        }

        /// <summary>
        /// Returns the specified color with the new alpha.
        /// </summary>
        /// <param name="color">The input color.</param>
        /// <param name="alpha">The output alpha.</param>
        /// <returns>A color with the specified alpha.</returns>
        public static Color RGB(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        public static void DisableGraphicAfterCoroutine(this Coroutine coroutine, Graphic graphic)
        {
            graphic.StartCoroutine(WaitForCoroutine());

            IEnumerator WaitForCoroutine()
            {
                yield return coroutine;
                
                graphic.enabled = false;
            }
        }

        private static float RangeClamp(float input, float inputLow, float inputHigh, float outputLow, float outputHigh)
        {
            var value = (input - inputLow) / (inputHigh - inputLow) * (outputHigh - outputLow) + outputLow;

            if (outputLow > outputHigh)
            {
                var t = outputLow;
                outputLow = outputHigh;
                outputHigh = t;
            }

            return Mathf.Clamp(value, outputLow, outputHigh);
        }        
    }
}
