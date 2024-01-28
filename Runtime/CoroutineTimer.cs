using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TezUI
{
    /// <summary>
    /// A custom yield instruction that can be used for specific timing 
    /// events in coroutines.
    /// </summary>
    public class CoroutineTimer : CustomYieldInstruction
    {
        /// <summary>
        /// When this timer is used in a coroutine, this is the amount of time 
        /// the coroutine will wait.
        /// </summary>
        public float Time { get; set; }
        /// <summary>
        /// The amount of time currently elapsed. When this reaches <see cref="Time"/>,
        /// the coroutine will stop waiting.
        /// </summary>
        public float ElapsedTime { get; private set; }
        /// <summary>
        /// The graphic being waited on, a graphic can only have one coroutine timer.
        /// </summary>
        public Graphic Graphic { get; set; }
		public override bool keepWaiting => ElapsedTime < Time;
		private readonly static Dictionary<Graphic, CoroutineTimer> _existingTimers = new Dictionary<Graphic, CoroutineTimer>();
               
        /// <summary>
        /// Instantiates a new timer on the specified graphic. A graphic can only 
        /// have one timer, so if the graphic already has a timer, this will return
        /// that timer instead.
        /// </summary>
        /// <param name="initialTime">The amount of time a coroutine will wait on 
        /// this timer for.</param>
        /// <param name="graphic">The graphic this timer will run on.</param>
        /// <returns>A coroutine timer running on the specified graphic.</returns>
        public static CoroutineTimer Create(float initialTime, Graphic graphic)
        {
            if (_existingTimers.ContainsKey(graphic))
            {
                var existingTimer = _existingTimers[graphic];
                existingTimer.Time = initialTime;
				existingTimer.ElapsedTime = 0;

				return existingTimer;
			}

            var timer = new CoroutineTimer();
            timer.Time = initialTime;
            timer.ElapsedTime = 0;
            timer.Graphic = graphic;

            _existingTimers.Add(graphic, timer);

            graphic.StartCoroutine(timer.TakeTimeCoroutine());

            return timer;
        }

        /// <summary>
        /// Resets the timer, extending the amount of time it will run for.
        /// </summary>
        public void Extend()
        {
            ElapsedTime = 0f;
        }

        /// <summary>
        /// Extends the duration by a specific amount of time.
        /// </summary>
        /// <param name="time">The amount of time in seconds to extend 
        /// the timer for.</param>
        public void ExtendBy(float time)
        {
            ElapsedTime -= time;
        }

        /// <summary>
        /// Stops and kills the timer. Any coroutines waiting on this 
        /// timer will continue.
        /// </summary>
        public void Stop()
        {
            ElapsedTime = Time;
            Graphic.StopCoroutine(TakeTimeCoroutine());
            _existingTimers.Remove(Graphic);
        }

        private IEnumerator TakeTimeCoroutine()
        {
            while (keepWaiting)
            {
                var oldTime = UnityEngine.Time.time;
                yield return new WaitForEndOfFrame();
                var newTime = UnityEngine.Time.time;

                ElapsedTime += newTime - oldTime;
            }

			_existingTimers.Remove(Graphic);
		}    
    }
}
