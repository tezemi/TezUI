using UnityEngine;

namespace TezUI
{
    public class WaitForTimer : CustomYieldInstruction
    {
        public CoroutineTimer CoroutineTimer { get; }
        public override bool keepWaiting => CoroutineTimer.ElapsedTime < CoroutineTimer.Time;

        public WaitForTimer(CoroutineTimer coroutineTimer)
        {
            CoroutineTimer = coroutineTimer;
        }
    }
}
