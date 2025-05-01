using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Tero.Utility
{
    public class Timer{
        public event Action OnTimerDone;
        public float startTime;
        private float duration;
        private float targetTime;
        public bool isActive;
        public Timer(float duration){
            this.duration = duration;
        }
        public void StartTimer()
        {
            startTime = Time.time*Time.deltaTime;
            targetTime = startTime + duration;
            isActive = true;
        }

        public void StopTimer()
        {
            isActive = false;
        }

        public void Tick()
        {
            if (!isActive) return;

            if (Time.time >= targetTime)
            {
                OnTimerDone?.Invoke();
                StopTimer();
            }
        }
    }
}
