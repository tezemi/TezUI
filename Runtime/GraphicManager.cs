using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TezUI
{
    internal static class GraphicManager
    {
        public static readonly Dictionary<(Graphic, EffectType), Coroutine> _runningCoroutines = new Dictionary<(Graphic, EffectType), Coroutine>();

        public static void Add(Graphic graphic, EffectType effectType, Coroutine coroutine)
        {
            if (!graphic.isActiveAndEnabled)
                return;

            var key = (graphic, effectType);

            if (IsRunning(graphic, effectType))
            {
                Remove(graphic, effectType);
            }

            _runningCoroutines.Add(key, coroutine);
        }

        public static void Remove(Graphic graphic, EffectType effectType)
        {
            var key = (graphic, effectType);

            graphic.StopCoroutine(_runningCoroutines[key]);
            _runningCoroutines.Remove(key);
        }

        public static bool IsRunning(Graphic graphic, EffectType effectType)
        {
            var key = (graphic, effectType);

            return _runningCoroutines.ContainsKey(key);
        }
    }
}
