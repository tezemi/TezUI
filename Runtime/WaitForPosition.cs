using UnityEngine;
using UnityEngine.UI;

namespace TezUI
{
    public class WaitForPosition : CustomYieldInstruction
    {
        public Graphic Graphic { get; }
        public Vector3 DestinationVector { get; }
        public override bool keepWaiting => Graphic.transform.position != DestinationVector && GraphicManager.IsRunning(Graphic, EffectType.SetPosition);

        public WaitForPosition(Graphic graphic, Vector3 destinationVector)
        {
            Graphic = graphic;
            DestinationVector = destinationVector;
        }
    }
}
