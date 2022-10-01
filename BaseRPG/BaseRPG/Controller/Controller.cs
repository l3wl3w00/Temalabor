using BaseRPG.Controller.Input;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Model.Game;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.System;

namespace BaseRPG.Controller
{
    public class Controller
    {
        private PlayerControl playerControl;
        private InputHandler inputHandler;
        public InputHandler InputHandler { get { return inputHandler; } }
        private bool running = true;
        public Game Game { get { return game; } }

        private Game game;
        public Controller(Game game)
        {
            this.game = game;
            playerControl = new PlayerControl(game.Hero,game.PhysicsFactory);
            inputHandler = new InputHandler(
                RawInputProcessedInputMapper.CreateDefault(),
                ProcessedInputActionMapper.CreateDefault(playerControl)
            );

        }
        public void MainLoop(CanvasControl canvas) {
            Stopwatch sw = Stopwatch.StartNew();
            double lastTickTime = 0;
            while (running) {
                var currentTime = sw.Elapsed.TotalMilliseconds;

                // 1 delta = 100 ms
                var delta = (currentTime - lastTickTime)/1000.0;
                if (delta < 0.000000001) {
                    Console.WriteLine(delta);
                }
                lastTickTime = currentTime;
                Tick(delta);
                //canvas.Invalidate();
            }
        }
        public void Tick(double delta) {

            inputHandler.OnTick();
            playerControl.OnTick(delta);
            game.CurrentWorld.OnTick();
        }

    }
}
