﻿using BaseRPG.View.Animation;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Utility
{
    public class DeltaLoopHandler
    {
        private bool isFirstTick = true;
        private Stopwatch stopWatch;

        public DeltaLoopHandler()
        {
            this.stopWatch = new();
            stopWatch.Start();
        }

        private double lastTickTime = 0.0;
        public event Action FirsTickEvent;

        public double Tick() {
            if (isFirstTick)
            {
                FirsTickEvent?.Invoke();
                isFirstTick = false;
            }
            var currentTime = stopWatch.Elapsed.TotalMilliseconds;
            double delta = (currentTime - lastTickTime) / 1000.0;
            lastTickTime = currentTime;
            return delta;
        }
    }
}
