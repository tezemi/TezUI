using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TezUI
{
    /// <summary>
    /// Provides effects for manipulating different aspects of a UI element's 
    /// transform component.
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Transitions the graphic's position to the specified one over time.
        /// </summary>
        /// <param name="graphic">The graphic that will be moved.</param>
        /// <param name="position">The position to move to.</param>
        /// <param name="time">The amount of time the effect will take.</param>
        /// <returns>Waits until the graphic is at the new position.</returns>
        public static WaitForPosition SetPosition(this Graphic graphic, Vector2 position, float time)
        {
            if (!graphic.isActiveAndEnabled)
            {
                Debug.LogWarning($"Can't run method {nameof(SetPosition)} on {graphic.name}, it is disabled.", graphic);

                return null;
            }
            
            var coroutine = graphic.StartCoroutine(SetPositionCoroutine());

            GraphicManager.Add(graphic, EffectType.SetColor, coroutine);

            return new WaitForPosition(graphic, position);

            IEnumerator SetPositionCoroutine()
            {
                var distance = Vector2.Distance(graphic.rectTransform.anchoredPosition, position);

                while (graphic.rectTransform.anchoredPosition != position)
                {
                    yield return new WaitForEndOfFrame();

                    graphic.rectTransform.anchoredPosition = Vector2.MoveTowards(graphic.rectTransform.anchoredPosition, position, Utils.GetDistanceFromTime(time, distance));
                }

                GraphicManager.Remove(graphic, EffectType.SetPosition);
            }
        }

        /// <summary>
        /// Transitions the graphic's scale to the specified one over time.
        /// </summary>
        /// <param name="graphic">The graphic that's scale will change.</param>
        /// <param name="scale">The new scale to move to.</param>
        /// <param name="time">The amount of time the effect takes.</param>
        /// <returns>Waits until the new scale is reached.</returns>
        public static WaitForScale SetScale(this Graphic graphic, Vector3 scale, float time)
        {
            if (!graphic.isActiveAndEnabled)
            {
                Debug.LogWarning($"Can't run method {nameof(SetScale)} on {graphic.name}, it is disabled.", graphic);

                return null;
            }
            
            var coroutine = graphic.StartCoroutine(SetScaleCoroutine());

            GraphicManager.Add(graphic, EffectType.SetScale, coroutine);

            return new WaitForScale(graphic, scale);

            IEnumerator SetScaleCoroutine()
            {
                var distance = Vector3.Distance(graphic.rectTransform.localScale, scale);

                while (graphic.rectTransform.localScale != scale)
                {
                    yield return new WaitForEndOfFrame();

                    graphic.rectTransform.localScale = Vector3.MoveTowards(graphic.rectTransform.localScale, scale, Utils.GetDistanceFromTime(time, distance));
                }

                GraphicManager.Remove(graphic, EffectType.SetScale);
            }
        }
    }
}
