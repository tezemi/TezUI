using UnityEngine;
using UnityEngine.UI;

namespace TezUI
{
    /// <summary>
    /// Waits until 
    /// </summary>
    public class WaitForFade : CustomYieldInstruction
    {
        public Graphic Graphic { get; }
        public Color DestinationColor { get; }
        public override bool keepWaiting => Graphic.color != DestinationColor && GraphicManager.IsRunning(Graphic, EffectType.SetColor);

        public WaitForFade(Graphic graphic, Color destinationColor)
        {
            Graphic = graphic;
            DestinationColor = destinationColor;
        }        
    }
}
