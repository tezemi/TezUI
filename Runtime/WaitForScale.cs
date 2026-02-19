using UnityEngine;
using UnityEngine.UI;

namespace TezUI
{
    public class WaitForScale : CustomYieldInstruction
    {
        public Graphic Graphic { get; }
        public Vector3 DestinationVector { get; }
        public override bool keepWaiting => Graphic.transform.localScale != DestinationVector || GraphicManager.IsRunning(Graphic, EffectType.SetScale);

        public WaitForScale(Graphic graphic, Vector3 destinationVector)
        {
            Graphic = graphic;
            DestinationVector = destinationVector;
        }
    }
}
