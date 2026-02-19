using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TezUI
{
    internal static class GraphicManager
    {
        public static readonly Dictionary<int, Coroutine> RunningCoroutines = new ();

        public static void Add(Graphic graphic, EffectType effectType, Coroutine coroutine)
        {
            if (!graphic.isActiveAndEnabled)
                return;

            int key = GetGraphicKey(graphic,  effectType);
            if (IsRunning(graphic, effectType))
            {
                Remove(graphic, effectType);
            }

            RunningCoroutines.Add(key, coroutine);
        }

        public static void Remove(Graphic graphic, EffectType effectType)
        {
            if (!IsRunning(graphic, effectType))
            {
                Debug.LogWarning($"Tried to stop graphic coroutine on '{graphic.name}' and effect '{effectType}', but it was not running.");
                
                return;
            }
            
            int key = GetGraphicKey(graphic,  effectType);

            graphic.StopCoroutine(RunningCoroutines[key]);
            RunningCoroutines.Remove(key);
        }

        public static bool IsRunning(Graphic graphic, EffectType effectType)
        {
            int key = GetGraphicKey(graphic,  effectType);

            return RunningCoroutines.ContainsKey(key);
        }

        private static int GetGraphicKey(Graphic graphic, EffectType effectType)
        {
            return graphic.GetInstanceID() + (int)effectType;
        }
    }
}
